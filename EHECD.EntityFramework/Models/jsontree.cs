using System.Collections.Generic;

namespace EHECD.EntityFramework.Models
{
    /// <summary>
    /// easy-ui树形控件的json数据格式
    /// </summary>
    public class jsontree
	{
		public long id
		{
			set;
			get;
		}

		public string text		 
		{
			set;
			get;
		}

	   public string state		  
		{
			set;
			get;
		}
		public List<jsontree> children
		{
			set;
			get;
		}
	}
}
