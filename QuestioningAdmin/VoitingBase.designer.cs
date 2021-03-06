﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuestioningAdmin
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Voiting")]
	public partial class VoitingBaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertQuestAnswers(QuestAnswers instance);
    partial void UpdateQuestAnswers(QuestAnswers instance);
    partial void DeleteQuestAnswers(QuestAnswers instance);
    partial void InsertQuestSettings(QuestSettings instance);
    partial void UpdateQuestSettings(QuestSettings instance);
    partial void DeleteQuestSettings(QuestSettings instance);
    #endregion
		
		public VoitingBaseDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["VoitingConnectionString1"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public VoitingBaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VoitingBaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VoitingBaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VoitingBaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<QuestAnswers> QuestAnswers
		{
			get
			{
				return this.GetTable<QuestAnswers>();
			}
		}
		
		public System.Data.Linq.Table<QuestSettings> QuestSettings
		{
			get
			{
				return this.GetTable<QuestSettings>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.QuestAnswers")]
	public partial class QuestAnswers : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<int> _AnketaId;
		
		private System.Nullable<int> _QuestionId;
		
		private System.Nullable<int> _EmpId;
		
		private System.Nullable<int> _EmpDepNum;
		
		private System.Nullable<int> _EmpDepPos;
		
		private System.Nullable<System.DateTime> _AnswerDate;
		
		private System.Nullable<int> _Result;
		
		private string _Ip;
		
		private string _CompName;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnAnketaIdChanging(System.Nullable<int> value);
    partial void OnAnketaIdChanged();
    partial void OnQuestionIdChanging(System.Nullable<int> value);
    partial void OnQuestionIdChanged();
    partial void OnEmpIdChanging(System.Nullable<int> value);
    partial void OnEmpIdChanged();
    partial void OnEmpDepNumChanging(System.Nullable<int> value);
    partial void OnEmpDepNumChanged();
    partial void OnEmpDepPosChanging(System.Nullable<int> value);
    partial void OnEmpDepPosChanged();
    partial void OnAnswerDateChanging(System.Nullable<System.DateTime> value);
    partial void OnAnswerDateChanged();
    partial void OnResultChanging(System.Nullable<int> value);
    partial void OnResultChanged();
    partial void OnIpChanging(string value);
    partial void OnIpChanged();
    partial void OnCompNameChanging(string value);
    partial void OnCompNameChanged();
    #endregion
		
		public QuestAnswers()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnketaId", DbType="Int")]
		public System.Nullable<int> AnketaId
		{
			get
			{
				return this._AnketaId;
			}
			set
			{
				if ((this._AnketaId != value))
				{
					this.OnAnketaIdChanging(value);
					this.SendPropertyChanging();
					this._AnketaId = value;
					this.SendPropertyChanged("AnketaId");
					this.OnAnketaIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuestionId", DbType="Int")]
		public System.Nullable<int> QuestionId
		{
			get
			{
				return this._QuestionId;
			}
			set
			{
				if ((this._QuestionId != value))
				{
					this.OnQuestionIdChanging(value);
					this.SendPropertyChanging();
					this._QuestionId = value;
					this.SendPropertyChanged("QuestionId");
					this.OnQuestionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmpId", DbType="Int")]
		public System.Nullable<int> EmpId
		{
			get
			{
				return this._EmpId;
			}
			set
			{
				if ((this._EmpId != value))
				{
					this.OnEmpIdChanging(value);
					this.SendPropertyChanging();
					this._EmpId = value;
					this.SendPropertyChanged("EmpId");
					this.OnEmpIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmpDepNum", DbType="Int")]
		public System.Nullable<int> EmpDepNum
		{
			get
			{
				return this._EmpDepNum;
			}
			set
			{
				if ((this._EmpDepNum != value))
				{
					this.OnEmpDepNumChanging(value);
					this.SendPropertyChanging();
					this._EmpDepNum = value;
					this.SendPropertyChanged("EmpDepNum");
					this.OnEmpDepNumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmpDepPos", DbType="Int")]
		public System.Nullable<int> EmpDepPos
		{
			get
			{
				return this._EmpDepPos;
			}
			set
			{
				if ((this._EmpDepPos != value))
				{
					this.OnEmpDepPosChanging(value);
					this.SendPropertyChanging();
					this._EmpDepPos = value;
					this.SendPropertyChanged("EmpDepPos");
					this.OnEmpDepPosChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnswerDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> AnswerDate
		{
			get
			{
				return this._AnswerDate;
			}
			set
			{
				if ((this._AnswerDate != value))
				{
					this.OnAnswerDateChanging(value);
					this.SendPropertyChanging();
					this._AnswerDate = value;
					this.SendPropertyChanged("AnswerDate");
					this.OnAnswerDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Result", DbType="Int")]
		public System.Nullable<int> Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				if ((this._Result != value))
				{
					this.OnResultChanging(value);
					this.SendPropertyChanging();
					this._Result = value;
					this.SendPropertyChanged("Result");
					this.OnResultChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ip", DbType="NVarChar(50)")]
		public string Ip
		{
			get
			{
				return this._Ip;
			}
			set
			{
				if ((this._Ip != value))
				{
					this.OnIpChanging(value);
					this.SendPropertyChanging();
					this._Ip = value;
					this.SendPropertyChanged("Ip");
					this.OnIpChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompName", DbType="NChar(255)")]
		public string CompName
		{
			get
			{
				return this._CompName;
			}
			set
			{
				if ((this._CompName != value))
				{
					this.OnCompNameChanging(value);
					this.SendPropertyChanging();
					this._CompName = value;
					this.SendPropertyChanged("CompName");
					this.OnCompNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.QuestSettings")]
	public partial class QuestSettings : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private System.Nullable<int> _ValueInt;
		
		private string _ValueStr;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnValueIntChanging(System.Nullable<int> value);
    partial void OnValueIntChanged();
    partial void OnValueStrChanging(string value);
    partial void OnValueStrChanged();
    #endregion
		
		public QuestSettings()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ValueInt", DbType="Int")]
		public System.Nullable<int> ValueInt
		{
			get
			{
				return this._ValueInt;
			}
			set
			{
				if ((this._ValueInt != value))
				{
					this.OnValueIntChanging(value);
					this.SendPropertyChanging();
					this._ValueInt = value;
					this.SendPropertyChanged("ValueInt");
					this.OnValueIntChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ValueStr", DbType="NVarChar(50)")]
		public string ValueStr
		{
			get
			{
				return this._ValueStr;
			}
			set
			{
				if ((this._ValueStr != value))
				{
					this.OnValueStrChanging(value);
					this.SendPropertyChanging();
					this._ValueStr = value;
					this.SendPropertyChanged("ValueStr");
					this.OnValueStrChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
