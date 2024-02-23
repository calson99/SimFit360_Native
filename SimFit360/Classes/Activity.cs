using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimFit360.Classes
{
	public class Activity
	{
		public int Id { get; set; }
		public TimeSpan Time { get; set; }
		public DateTime Date { get; set; }
		public int Kcal { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int MaschineId { get; set; }
		public Maschine Maschine { get; set; }
	}
}
