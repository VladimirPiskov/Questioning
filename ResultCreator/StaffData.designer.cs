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

namespace ResultCreator
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="CHKComp")]
	public partial class StaffDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertStaffposition(Staffposition instance);
    partial void UpdateStaffposition(Staffposition instance);
    partial void DeleteStaffposition(Staffposition instance);
    partial void InsertStaffEmployee(StaffEmployee instance);
    partial void UpdateStaffEmployee(StaffEmployee instance);
    partial void DeleteStaffEmployee(StaffEmployee instance);
    partial void InsertStaffDepartments(StaffDepartments instance);
    partial void UpdateStaffDepartments(StaffDepartments instance);
    partial void DeleteStaffDepartments(StaffDepartments instance);
    #endregion
		
		public StaffDataDataContext() : 
				base(global::ResultCreator.Properties.Settings.Default.CHKCompConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public StaffDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public StaffDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public StaffDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public StaffDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Staffposition> Staffposition
		{
			get
			{
				return this.GetTable<Staffposition>();
			}
		}
		
		public System.Data.Linq.Table<StaffEmployee> StaffEmployee
		{
			get
			{
				return this.GetTable<StaffEmployee>();
			}
		}
		
		public System.Data.Linq.Table<StaffDepartments> StaffDepartments
		{
			get
			{
				return this.GetTable<StaffDepartments>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Staffposition")]
	public partial class Staffposition : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _POSITION_ID;
		
		private string _NAME;
		
		private EntityRef<Staffposition> _Staffposition2;
		
		private EntityRef<Staffposition> _Staffposition1;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPOSITION_IDChanging(int value);
    partial void OnPOSITION_IDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    #endregion
		
		public Staffposition()
		{
			this._Staffposition2 = default(EntityRef<Staffposition>);
			this._Staffposition1 = default(EntityRef<Staffposition>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_POSITION_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int POSITION_ID
		{
			get
			{
				return this._POSITION_ID;
			}
			set
			{
				if ((this._POSITION_ID != value))
				{
					if (this._Staffposition1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPOSITION_IDChanging(value);
					this.SendPropertyChanging();
					this._POSITION_ID = value;
					this.SendPropertyChanged("POSITION_ID");
					this.OnPOSITION_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="VarChar(100)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Staffposition_Staffposition", Storage="_Staffposition2", ThisKey="POSITION_ID", OtherKey="POSITION_ID", IsUnique=true, IsForeignKey=false)]
		public Staffposition Staffposition2
		{
			get
			{
				return this._Staffposition2.Entity;
			}
			set
			{
				Staffposition previousValue = this._Staffposition2.Entity;
				if (((previousValue != value) 
							|| (this._Staffposition2.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Staffposition2.Entity = null;
						previousValue.Staffposition1 = null;
					}
					this._Staffposition2.Entity = value;
					if ((value != null))
					{
						value.Staffposition1 = this;
					}
					this.SendPropertyChanged("Staffposition2");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Staffposition_Staffposition", Storage="_Staffposition1", ThisKey="POSITION_ID", OtherKey="POSITION_ID", IsForeignKey=true)]
		public Staffposition Staffposition1
		{
			get
			{
				return this._Staffposition1.Entity;
			}
			set
			{
				Staffposition previousValue = this._Staffposition1.Entity;
				if (((previousValue != value) 
							|| (this._Staffposition1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Staffposition1.Entity = null;
						previousValue.Staffposition2 = null;
					}
					this._Staffposition1.Entity = value;
					if ((value != null))
					{
						value.Staffposition2 = this;
						this._POSITION_ID = value.POSITION_ID;
					}
					else
					{
						this._POSITION_ID = default(int);
					}
					this.SendPropertyChanged("Staffposition1");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.StaffEmployee")]
	public partial class StaffEmployee : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SUBDIVISION_ID;
		
		private int _EMPLOYEE_ID;
		
		private string _LAST_NAME;
		
		private string _FIRST_NAME;
		
		private string _MIDDLE_NAME;
		
		private int _Position_ID;
		
		private System.Nullable<System.DateTime> _Entry_Date;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSUBDIVISION_IDChanging(int value);
    partial void OnSUBDIVISION_IDChanged();
    partial void OnEMPLOYEE_IDChanging(int value);
    partial void OnEMPLOYEE_IDChanged();
    partial void OnLAST_NAMEChanging(string value);
    partial void OnLAST_NAMEChanged();
    partial void OnFIRST_NAMEChanging(string value);
    partial void OnFIRST_NAMEChanged();
    partial void OnMIDDLE_NAMEChanging(string value);
    partial void OnMIDDLE_NAMEChanged();
    partial void OnPosition_IDChanging(int value);
    partial void OnPosition_IDChanged();
    partial void OnEntry_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnEntry_DateChanged();
    #endregion
		
		public StaffEmployee()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUBDIVISION_ID", DbType="Int NOT NULL")]
		public int SUBDIVISION_ID
		{
			get
			{
				return this._SUBDIVISION_ID;
			}
			set
			{
				if ((this._SUBDIVISION_ID != value))
				{
					this.OnSUBDIVISION_IDChanging(value);
					this.SendPropertyChanging();
					this._SUBDIVISION_ID = value;
					this.SendPropertyChanged("SUBDIVISION_ID");
					this.OnSUBDIVISION_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMPLOYEE_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int EMPLOYEE_ID
		{
			get
			{
				return this._EMPLOYEE_ID;
			}
			set
			{
				if ((this._EMPLOYEE_ID != value))
				{
					this.OnEMPLOYEE_IDChanging(value);
					this.SendPropertyChanging();
					this._EMPLOYEE_ID = value;
					this.SendPropertyChanged("EMPLOYEE_ID");
					this.OnEMPLOYEE_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LAST_NAME", DbType="VarChar(60)")]
		public string LAST_NAME
		{
			get
			{
				return this._LAST_NAME;
			}
			set
			{
				if ((this._LAST_NAME != value))
				{
					this.OnLAST_NAMEChanging(value);
					this.SendPropertyChanging();
					this._LAST_NAME = value;
					this.SendPropertyChanged("LAST_NAME");
					this.OnLAST_NAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FIRST_NAME", DbType="VarChar(60)")]
		public string FIRST_NAME
		{
			get
			{
				return this._FIRST_NAME;
			}
			set
			{
				if ((this._FIRST_NAME != value))
				{
					this.OnFIRST_NAMEChanging(value);
					this.SendPropertyChanging();
					this._FIRST_NAME = value;
					this.SendPropertyChanged("FIRST_NAME");
					this.OnFIRST_NAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MIDDLE_NAME", DbType="VarChar(60)")]
		public string MIDDLE_NAME
		{
			get
			{
				return this._MIDDLE_NAME;
			}
			set
			{
				if ((this._MIDDLE_NAME != value))
				{
					this.OnMIDDLE_NAMEChanging(value);
					this.SendPropertyChanging();
					this._MIDDLE_NAME = value;
					this.SendPropertyChanged("MIDDLE_NAME");
					this.OnMIDDLE_NAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Position_ID", DbType="Int NOT NULL")]
		public int Position_ID
		{
			get
			{
				return this._Position_ID;
			}
			set
			{
				if ((this._Position_ID != value))
				{
					this.OnPosition_IDChanging(value);
					this.SendPropertyChanging();
					this._Position_ID = value;
					this.SendPropertyChanged("Position_ID");
					this.OnPosition_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Entry_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Entry_Date
		{
			get
			{
				return this._Entry_Date;
			}
			set
			{
				if ((this._Entry_Date != value))
				{
					this.OnEntry_DateChanging(value);
					this.SendPropertyChanging();
					this._Entry_Date = value;
					this.SendPropertyChanged("Entry_Date");
					this.OnEntry_DateChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.StaffDepartments")]
	public partial class StaffDepartments : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _DepID;
		
		private string _DepName;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDepIDChanging(int value);
    partial void OnDepIDChanged();
    partial void OnDepNameChanging(string value);
    partial void OnDepNameChanged();
    #endregion
		
		public StaffDepartments()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int DepID
		{
			get
			{
				return this._DepID;
			}
			set
			{
				if ((this._DepID != value))
				{
					this.OnDepIDChanging(value);
					this.SendPropertyChanging();
					this._DepID = value;
					this.SendPropertyChanged("DepID");
					this.OnDepIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepName", DbType="NVarChar(50)")]
		public string DepName
		{
			get
			{
				return this._DepName;
			}
			set
			{
				if ((this._DepName != value))
				{
					this.OnDepNameChanging(value);
					this.SendPropertyChanging();
					this._DepName = value;
					this.SendPropertyChanged("DepName");
					this.OnDepNameChanged();
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