using System;
using System.Collections.Generic;
using System.Linq;
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

		public KestraaUploadRequest(byte[] fileToUpload, String fileType, String fileReference, String issueDate, String userId)
		{
			this.fileToUpload = fileToUpload;
			this.fileType = fileType;
			this.fileReference = fileReference;
			this.issueDate = issueDate;
			this.userId = userId;
		}
	}
}
