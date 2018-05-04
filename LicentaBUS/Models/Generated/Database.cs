




















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `NoBordersConnection`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=noBorders;Integrated Security=True`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace NoBordersConnection
{

	public partial class NoBordersConnectionDB : Database
	{
		public NoBordersConnectionDB() 
			: base("NoBordersConnection")
		{
			CommonConstruct();
		}

		public NoBordersConnectionDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			NoBordersConnectionDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static NoBordersConnectionDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new NoBordersConnectionDB();
        }

		[ThreadStatic] static NoBordersConnectionDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static NoBordersConnectionDB repo { get { return NoBordersConnectionDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    

	[TableName("dbo.CANDIDATE_PROCESS")]



	[PrimaryKey("id_process")]




	[ExplicitColumns]

    public partial class CANDIDATE_PROCESS : NoBordersConnectionDB.Record<CANDIDATE_PROCESS>  
    {



		[Column] public int id_candidate { get; set; }





		[Column] public int id_company { get; set; }





		[Column] public int id_status { get; set; }





		[Column] public int id_process { get; set; }



	}

    

	[TableName("dbo.CANDIDATE_STATUS")]



	[PrimaryKey("id_status")]




	[ExplicitColumns]

    public partial class CANDIDATE_STATUS : NoBordersConnectionDB.Record<CANDIDATE_STATUS>  
    {



		[Column] public int id_status { get; set; }





		[Column] public string status { get; set; }



	}

    

	[TableName("dbo.CANDIDATES")]



	[PrimaryKey("id_candidate")]




	[ExplicitColumns]

    public partial class CANDIDATE : NoBordersConnectionDB.Record<CANDIDATE>  
    {



		[Column] public int id_candidate { get; set; }





		[Column] public string last_name { get; set; }





		[Column] public string first_name { get; set; }





		[Column] public string skills { get; set; }





		[Column] public string email { get; set; }





		[Column] public int language { get; set; }





		[Column] public int experience { get; set; }





		[Column] public int education { get; set; }





		[Column] public string hobbies { get; set; }





		[Column] public int? saved_profile { get; set; }





		[Column] public string password { get; set; }



	}

    

	[TableName("dbo.COMPANIES")]



	[PrimaryKey("id_company")]




	[ExplicitColumns]

    public partial class COMPANY : NoBordersConnectionDB.Record<COMPANY>  
    {



		[Column] public int id_company { get; set; }





		[Column] public string company_name { get; set; }





		[Column] public string company_website { get; set; }





		[Column] public string CEO { get; set; }





		[Column] public int? no_employees { get; set; }





		[Column] public string city { get; set; }





		[Column] public DateTime? date_creation { get; set; }





		[Column] public int? annual_income { get; set; }





		[Column] public string description { get; set; }





		[Column] public string technical_stack { get; set; }





		[Column] public int spoken_language { get; set; }





		[Column] public string social_network { get; set; }



	}

    

	[TableName("dbo.EDUCATIONS")]



	[PrimaryKey("id_education")]




	[ExplicitColumns]

    public partial class EDUCATION : NoBordersConnectionDB.Record<EDUCATION>  
    {



		[Column] public int id_education { get; set; }





		[Column] public string school { get; set; }





		[Column] public string diploma { get; set; }





		[Column] public DateTime from_date { get; set; }





		[Column] public DateTime to_date { get; set; }





		[Column] public string description { get; set; }





		[Column] public int id_candidate { get; set; }



	}

    

	[TableName("dbo.EXPERIENCES")]



	[PrimaryKey("id_experience")]




	[ExplicitColumns]

    public partial class EXPERIENCE : NoBordersConnectionDB.Record<EXPERIENCE>  
    {



		[Column] public int id_experience { get; set; }





		[Column] public string company_name { get; set; }





		[Column] public DateTime from_date { get; set; }





		[Column] public DateTime to_date { get; set; }





		[Column] public string description { get; set; }





		[Column] public int id_candidate { get; set; }



	}

    

	[TableName("dbo.LANGUAGES")]



	[PrimaryKey("id_language")]




	[ExplicitColumns]

    public partial class LANGUAGE : NoBordersConnectionDB.Record<LANGUAGE>  
    {



		[Column] public int id_language { get; set; }





		[Column] public string language { get; set; }



	}

    

	[TableName("dbo.LOGIN")]



	[PrimaryKey("email", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class LOGIN : NoBordersConnectionDB.Record<LOGIN>  
    {



		[Column] public string email { get; set; }





		[Column] public string password { get; set; }



	}

    

	[TableName("dbo.REGISTRATION_CANDIDATE")]



	[PrimaryKey("id_candidate_registration")]




	[ExplicitColumns]

    public partial class REGISTRATION_CANDIDATE : NoBordersConnectionDB.Record<REGISTRATION_CANDIDATE>  
    {



		[Column] public string email { get; set; }





		[Column] public string first_name { get; set; }





		[Column] public string last_name { get; set; }





		[Column] public string password { get; set; }





		[Column] public int id_candidate_registration { get; set; }



	}

    

	[TableName("dbo.REGISTRATION_COMPANY")]



	[PrimaryKey("id_company_registration")]




	[ExplicitColumns]

    public partial class REGISTRATION_COMPANY : NoBordersConnectionDB.Record<REGISTRATION_COMPANY>  
    {



		[Column] public string company_name { get; set; }





		[Column] public string email { get; set; }





		[Column] public string first_name { get; set; }





		[Column] public string last_name { get; set; }





		[Column] public string password { get; set; }





		[Column] public int id_company_registration { get; set; }



	}


}