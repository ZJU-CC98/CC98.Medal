using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 定义一个用户。
	/// </summary>
	[Table("Users")]
	public class User
	{
		/// <summary>
		/// 用户的标识。
		/// </summary>
		[Column("UserId")]
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 用户的财富值。
		/// </summary>
		[Column("UserWealth")]
		public int Wealth { get; set; }
	}
}