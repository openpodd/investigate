using NUnit.Framework;
using System;
using System.Collections.Generic;
using Investigate;
using System.Threading;
using System.Threading.Tasks;

namespace TestCore
{
	public class StubPoddService : IPoddService
	{
		public Object SearchLock = new Object();
		public TaskCompletionSource<bool> GuardBeforeSearch = new TaskCompletionSource<bool>();
		public TaskCompletionSource<bool> GuardContinueSearch = new TaskCompletionSource<bool>();

		public Task<AuthorityResult> GetAuthorities()
		{
			throw new NotImplementedException();
		}

		public Task<LoginResult> Login(LoginRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<SearchResult> Search(SearchRequest searchRequest)
		{
		    GuardBeforeSearch.SetResult(true);
		    await GuardContinueSearch.Task;

		    return new SearchResult()
			{
				Results = new SearchItem[2]
				{
				    new SearchItem() { Id = 1 },
				    new SearchItem() { Id = 2 }
				}
			};
		}
	}

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestCase_ReportSelectionViewModel_SearchCommand()
        {
            var stubPoddService = new StubPoddService();
            var viewModel = new ReportSelectionViewModel()
            {
                PoddService = stubPoddService
            };
            Assert.True(viewModel.SearchCommand.CanExecute(null));
            Assert.AreEqual(0, viewModel.Reports.Count, "Should has 0 reports when init");

            var task = Task.Run(() =>
            {
                Console.WriteLine("Before execute SearchCommand");
                viewModel.SearchCommand.Execute(null);
                Console.WriteLine("After execute SearchCommand");

                Thread.Sleep(10);
                Assert.True(viewModel.SearchCommand.CanExecute(null));
                Assert.AreEqual(2, viewModel.Reports.Count, "Should has 2 reports fetched");
                Console.WriteLine("After test SearchCommand.CanExecute #2");
            });

            stubPoddService.GuardBeforeSearch.Task.Wait();
            Assert.False(viewModel.SearchCommand.CanExecute(null), "Should not allow SearchCommand execute");
            stubPoddService.GuardContinueSearch.SetResult(true);
            Console.WriteLine("After test SearchCommand.CanExecute #1");

            task.Wait();
        }

        [Test]
        public void TestCase_ReportSelectionViewModel_SelectReportCommand()
        {
            var stubPoddService = new StubPoddService();
            var viewModel = new ReportSelectionViewModel()
            {
                PoddService = stubPoddService
            };

            var searchItem1 = new SearchItem() {Id = 1};
            var searchItem2 = new SearchItem() {Id = 2};

            Assert.AreEqual(0, viewModel.NumberOfSelectedReports);
            viewModel.SelectReportCommand.Execute(searchItem1);

            Assert.AreEqual(1, viewModel.NumberOfSelectedReports, "Should has 1 item selected");
            viewModel.SelectReportCommand.Execute(searchItem1);

            Assert.AreEqual(1, viewModel.NumberOfSelectedReports,
                "Should has 1 item selected after re-select the same item");

            viewModel.SelectReportCommand.Execute(searchItem2);
            Assert.AreEqual(2, viewModel.NumberOfSelectedReports, "Should has 2 item selected");
        }

        [Test]
        public void TestCase_ReportSelectionViewModel_DoneSelectionCommand()
        {
            var stubPoddService = new StubPoddService();
            var viewModel = new ReportSelectionViewModel()
            {
                PoddService = stubPoddService
            };

            var searchItem1 = new SearchItem() {Id = 1};
            var searchItem2 = new SearchItem() {Id = 2};

            viewModel.SelectReportCommand.Execute(searchItem1);
            viewModel.SelectReportCommand.Execute(searchItem2);

            TaskCompletionSource<bool> guardDoneSelection = new TaskCompletionSource<bool>();
            viewModel.DoneReportSelection = (_searchItems) =>
            {
                guardDoneSelection.SetResult(true);
                Assert.AreEqual(2, _searchItems.Count);
            };
            viewModel.ReportSelectionDoneCommand.Execute(null);
            guardDoneSelection.Task.Wait();
        }
    }
}
