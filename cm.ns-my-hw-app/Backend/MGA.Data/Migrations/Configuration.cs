//namespace MGA.Infra.Data.Migrations
//{
//	using MGA.Domain.Models;
//	using MGA.Data.Context;
//	using System;
//	using System.Data.Entity;
//	using System.Data.Entity.Migrations;
//	using System.Linq;

//	internal sealed class Configuration : DbMigrationsConfiguration<DWSPContext>
//	{
//		public Configuration()
//		{
//			AutomaticMigrationsEnabled = true;
//		}

//		protected override void Seed(DWSPContext context)
//		{
//			context.Users.AddOrUpdate(
//			s => s.UserName,
//			  new User { ID = 1, UserName = "Leonardo", Domain = "hitachiconsulting", DateCreated = DateTime.Now, IsActive = true },
//			  new User { ID = 2, UserName = "Paulo", Domain = "hitachiconsulting", DateCreated = DateTime.Now, IsActive = true },
//			  new User { ID = 3, UserName = "Bruna", Domain = "hitachiconsulting", DateCreated = DateTime.Now, IsActive = true },
//			  new User { ID = 4, UserName = "Sarah", Domain = "hitachiconsulting hk", DateCreated = DateTime.Now, IsActive = true }
//			);

//			context.ParameterTypes.AddOrUpdate(
//			s => s.Code,
//			  new ParameterType { Code = "R", WaterType = "Raw", DateCreated = DateTime.Now, UserID = 1 },
//			  new ParameterType { Code = "T", WaterType = "Treated", DateCreated = DateTime.Now, UserID = 1 },
//			  new ParameterType { Code = "R/T", WaterType = "Raw/Treated", DateCreated = DateTime.Now, UserID = 1 },
//			  new ParameterType { Code = "PWS", WaterType = "Private Water Supply", DateCreated = DateTime.Now, UserID = 1 },
//			  new ParameterType { Code = "H", WaterType = "Hazard", DateCreated = DateTime.Now, UserID = 1 }
//			);

//			context.Stages.AddOrUpdate(
//			s => s.Name,
//			  new Stage { Name = "Catchment", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new Stage { Name = "Abstraction", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new Stage { Name = "Consumer", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new Stage { Name = "Distribution", Status = true, DateCreated = DateTime.Now, UserID = 2 },
//			  new Stage { Name = "Storage", Status = true, DateCreated = DateTime.Now, UserID = 3 },
//			  new Stage { Name = "Treatment", Status = true, DateCreated = DateTime.Now, UserID = 2 },
//			  new Stage { Name = "Bulk Supply", Status = true, DateCreated = DateTime.Now, UserID = 1 }
//			);

//			//DWI Units Of Measure List
//			context.UnitOfMeasures.AddOrUpdate(
//			s => s.UOM,
//			new UnitOfMeasure { UOM = "mg/l Pt/Co", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "NTU", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No.", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "deg ºC", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "pH value", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l SO4", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l Mg", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l Na", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l K", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "ug/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l NO3", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l NO2", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l NH4", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l N", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l NH3", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l C", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l as Lauryl Sulphate", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Al", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Fe", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Mn", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l Cu", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Zn", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l P/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l F", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l As", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Cd", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l CN", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Cr", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Hg", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Ni", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Pb", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Sb", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Se", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No. /100ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No. /250ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No. /250ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Count", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No m/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Litre", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μS/cm @ 20 ºC", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "%", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l Ca", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l B", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l Ba", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l CaC03", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l HCO3", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "μg/l BrO3", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Bq/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Product spec.", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "µg/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l as CO2", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "mg/l as O2", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Areal Units/ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Cells/ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No. ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "µg/l Br", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "µg/l Mo", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "No. 1/ml", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "µg/l U", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "abs/m", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "ng/l", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "Bq/kg", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "count/unit vol.", DateCreated = DateTime.Now, UserID = 1 },
//			new UnitOfMeasure { UOM = "% transmission", DateCreated = DateTime.Now, UserID = 1 }
//			);

//			context.SourceTypes.AddOrUpdate(
//			s => s.Name,
//			  new SourceType { Code = "SW", Name = "Surface Water", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new SourceType { Code = "GW", Name = "Ground Water", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new SourceType { Code = "MW", Name = "Mixed Water", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new SourceType { Code = "N/A", Name = "Not Applicable", Status = true, DateCreated = DateTime.Now, UserID = 2 }
//			);

//			context.AssetsStatus.AddOrUpdate(
//			s => s.Name,
//			  new AssetStatus { Name = "Active", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetStatus { Name = "Inactive", Status = true, DateCreated = DateTime.Now, UserID = 1 }
//			);

//			context.ResponsibleManagers.AddOrUpdate(
//			s => s.ADUsername,
//			  new ResponsibleManager { ADUsername = "dthorne", FirstName = "Duncan", LastName = "Thorne", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "swills", FirstName = "Steve", LastName = "Wills", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "twilson", FirstName = "Terry ", LastName = "Wilson", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "hkeeble", FirstName = "Helen", LastName = "Keeble", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "tcoates", FirstName = "Tim", LastName = "Coates", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "atildesl", FirstName = "Adam", LastName = "Tildesley", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "pbayliss", FirstName = "Paul", LastName = "Bayliss", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "agocher", FirstName = "Anna", LastName = "Gocher", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "rgilford", FirstName = "Roger", LastName = "Gilford", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "arossiter", FirstName = "Annette", LastName = "Rossiter", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "aminnis", FirstName = "Alex", LastName = "Minnis", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "tball", FirstName = "Tim", LastName = "Ball", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "rhaffron", FirstName = "Ray", LastName = "Haffron", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "jpharvey", FirstName = "Jason", LastName = "Harvey", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "crockey", FirstName = "Chris", LastName = "Rockey", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "mshaw", FirstName = "Matthew", LastName = "Shaw", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "ctinkler", FirstName = "Colin", LastName = "Tinkler", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "mstreet", FirstName = "Marc", LastName = "Street", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "jskipwith", FirstName = "Jon", LastName = "Skipwith", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "mbroom", FirstName = "Martin ", LastName = "Broom", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "ndade", FirstName = "Nicholas", LastName = "Dade", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "shooper", FirstName = "Stuart", LastName = "Hooper", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "atrethew", FirstName = "Annie", LastName = "Trethewey", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "abowen", FirstName = "Anthony", LastName = "Bowen", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "mjervis", FirstName = "Mike", LastName = "Jervis", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "mkey", FirstName = "Myles", LastName = "Key", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "mcopperw", FirstName = "Mike", LastName = "Copperwhite", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			new ResponsibleManager { ADUsername = "gfurse", FirstName = "Gary", LastName = "Furse", Status = true, DateCreated = DateTime.Now, UserID = 1 }

//			);

//			context.AssetTypes.AddOrUpdate(
//			s => s.Name,
//			  new AssetType { Name = "Company", Hierarchy = 1, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Distribution main", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Trunk main", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Supply system", Hierarchy = 5, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Water Supply Zone", Hierarchy = 6, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Catchment", Hierarchy = 3, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "External supplier", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Private water supply", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Private distribution system", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Pumping station", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Drinking water production", Hierarchy = 10, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Drinking water distribution", Hierarchy = 10, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Water treatment works", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Service reservoir", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "WIS zone", Hierarchy = 7, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "DMA", Hierarchy = 20, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Domestic consumer", Hierarchy = 25, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Non-domestic consumer", Hierarchy = 25, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Abstraction point", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new AssetType { Name = "Supply Point", Hierarchy = 15, Status = true, DateCreated = DateTime.Now, UserID = 1 }
//			);

//			context.WaterQualities.AddOrUpdate(
//			s => s.Name,
//			  new WaterQualityArea { Name = "Exeter", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new WaterQualityArea { Name = "Plymouth", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new WaterQualityArea { Name = "Cornwall", Status = true, DateCreated = DateTime.Now, UserID = 1 },
//			  new WaterQualityArea { Name = "Bournemouth", Status = true, DateCreated = DateTime.Now, UserID = 1 }
//			);

//			//context.Parameters.AddOrUpdate(
//			//s => s.Name,
//			//  new Parameter
//			//  {
//			//  ID = 1,
//			//  Code = "123",
//			//  Name = "name",
//			//  Active = true,
//			//  IsKeyRisk = true,
//			//  IsReportable = true,
//			//  UnitOfMeasureID = 1,
//			//  ParameterTypeID = 1,
//			//  MeasureValue = 1,
//			//  DateCreated = DateTime.Now,
//			//  DateExcluded = DateTime.Now,
//			//  UserID = 1
//			//  }
//			//);

//			//context.Consequences.AddOrUpdate(
//			//s => s.ID,
//			//  new Consequence
//			//  {
//			//  Value = 1,
//			//  PhaseID = 1,
//			//  ParameterID = 1,
//			//  DateCreated = DateTime.Now,
//			//  UserID = 1
//			//  }
//			//);
//		}
//	}
//}
