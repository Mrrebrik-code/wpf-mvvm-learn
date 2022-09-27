using System.Collections.Generic;

namespace CV19.Models.Decanat
{
	internal class Group
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Student> Students { get; set; }
	}
}