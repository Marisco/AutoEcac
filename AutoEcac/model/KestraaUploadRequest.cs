using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AutoEcac.model
{
	public class KestraaUploadRequest
	{
		public byte[] fileToUpload { get; set; }
		public string fileType { get; set; }
		public string fileReference { get; set; }
		public string issueDate { get; set; }
		public string userId { get; set; }
		
		public string fileName { get; set; }

		public KestraaUploadRequest(string fileName, byte[] fileToUpload, String fileType, String fileReference, String issueDate, String userId)
		{
			this.fileName = fileName;
			this.fileToUpload = fileToUpload;
			this.fileType = fileType;
			this.fileReference = fileReference;
			this.issueDate = issueDate;
			this.userId = userId;
		}

		public MultipartFormDataContent getFormContent()
		{
			var fileToUploadContent = new ByteArrayContent(fileToUpload);
			fileToUploadContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");

			return new MultipartFormDataContent
			{
				{ fileToUploadContent, "fileToUpload", fileName},
				{ new StringContent(fileType), "fileType"},
				{ new StringContent(fileReference), "fileReference"},
				{ new StringContent(issueDate), "issueDate"},
				{ new StringContent(userId), "userId"}
			};
		}
	}
}
