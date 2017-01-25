using System;
using Newtonsoft.Json;

namespace Investigate
{


	/**
	 * 
	 * {
     *	"count": 2149,
     *	"next": "?page=2",
     *	"previous": null,
     *	"results": [
     *	    {
     *	        "id": 174282,
     *	        "reportId": 267,
     *	        "guid": "a02e7e09-d960-4c59-be3c-e6f2a08e9d8d",
     *	        "reportTypeId": 2,
     *	        "reportTypeName": "สัตว์ป่วย/ตาย",
     *	        "date": "2017-01-21T11:02:48Z",
     *	        "administrationAreaId": 31,
     *	        "negative": true,
     *	        "incidentDate": "2017-01-21",
     *	        "createdBy": "760",
     *	        "createdByName": "ทนงศักดิ์ ปวงสายใจ",
     *	        "flag": "",
     *	        "testFlag": false,
     *	        "formDataExplanation": "<p>พบ ไก่พื้นเมือง[สัตว์ปีก] ป่วยจำนวน 0 ตัว, ตายจำนวน 0 ตัว จากทั้งหมด 150 ตัว จำนวนเล้า/คอกข้างเคียงที่มีอาการ 0\r\nโดยคาดว่าจะเป็นโรค ห่าไก่ มีอาการดังนี้ แพร่ระบาดรวดเร็ว\r\n</p>\r\n",
     *	        "renderedOriginalFormData": "<p>พบ ไก่พื้นเมือง[สัตว์ปีก] ป่วยจำนวน 0 ตัว, ตายจำนวน 0 ตัว จากทั้งหมด 150 ตัว จำนวนเล้า/คอกข้างเคียงที่มีอาการ 0\r\nโดยคาดว่าจะเป็นโรค ห่าไก่ มีอาการดังนี้ แพร่ระบาดรวดเร็ว\r\n</p>\r\n",
     *	        "administrationAreaAddress": "องค์การบริหารส่วนตำบลแม่ทา อำเภอแม่ออน",
     *	        "firstImageThumbnail": "",
     *	        "state": 24,
     *	        "stateCode": "report",
     *	        "stateName": "Report",
     *	        "parent": 173801,
     *	        "tags": null,
     *	        "reportLocation": [
     *	            99.2766201,
     *	            18.5997215
     *	        ]
     *	    },
     *	    
	 */
	[JsonObject(MemberSerialization.OptIn)]
	public class SearchResult
	{

		public bool Success;
		public string ErrorMessage;

		[JsonProperty("count")]
		public int Count;

		[JsonProperty("next")]
		public string NextUri;

		[JsonProperty("results")]
		public SearchItem[] results;

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SearchItem
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("reportTypeName")]
		public string ReportTypeName { get; set; }

		[JsonProperty("stateName")]
		public string ReportStateName { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("incidentDate")]
		public DateTime IncidentDate { get; set; }

		[JsonProperty("administrationAreaAddress")]
		public String AdministrationAreaName { get; set; }

		[JsonProperty("createdByName")]
		public String CreateByName { get; set; }

		[JsonProperty("createdByContact")]
		public String CreateByContact { get; set; }

		[JsonProperty("createdByTelephone")]
		public String CreateByTelephone { get; set; }

		[JsonProperty("renderedOriginalFormData")]
		public String RendererFormData { get; set; }
	}
}
