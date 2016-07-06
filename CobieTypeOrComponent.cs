// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool Xbim.CodeGeneration 
//  
//     Changes to this file may cause incorrect behaviour and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Common.Exceptions;
using Xbim.CobieExpress.Interfaces;
using Xbim.CobieExpress;
//## Custom using statements
//##

namespace Xbim.CobieExpress.Interfaces
{
	/// <summary>
    /// Readonly interface for CobieTypeOrComponent
    /// </summary>
	// ReSharper disable once PartialTypeWithSinglePart
	public partial interface @ICobieTypeOrComponent : ICobieAsset
	{
		IItemSet<ICobieTypeOrComponent> @AssemblyOf { get; }
		IEnumerable<ICobieConnection> @Connecting {  get; }
		IEnumerable<ICobieConnection> @ConnectedBefore {  get; }
		IEnumerable<ICobieConnection> @ConnectedAfter {  get; }
	
	}
}

namespace Xbim.CobieExpress
{
	[ExpressType("TypeOrComponent", 20)]
	// ReSharper disable once PartialTypeWithSinglePart
	public abstract partial class @CobieTypeOrComponent : CobieAsset, ICobieTypeOrComponent, IEquatable<@CobieTypeOrComponent>
	{
		#region ICobieTypeOrComponent explicit implementation
		IItemSet<ICobieTypeOrComponent> ICobieTypeOrComponent.AssemblyOf { 
			get { return new Common.Collections.ProxyItemSet<CobieTypeOrComponent, ICobieTypeOrComponent>( @AssemblyOf); } 
		}	
		 
		IEnumerable<ICobieConnection> ICobieTypeOrComponent.Connecting {  get { return @Connecting; } }
		IEnumerable<ICobieConnection> ICobieTypeOrComponent.ConnectedBefore {  get { return @ConnectedBefore; } }
		IEnumerable<ICobieConnection> ICobieTypeOrComponent.ConnectedAfter {  get { return @ConnectedAfter; } }
		#endregion

		//internal constructor makes sure that objects are not created outside of the model/ assembly controlled area
		internal CobieTypeOrComponent(IModel model, int label, bool activated) : base(model, label, activated)  
		{
			_assemblyOf = new OptionalItemSet<CobieTypeOrComponent>( this, 0,  13);
		}

		#region Explicit attribute fields
		private readonly OptionalItemSet<CobieTypeOrComponent> _assemblyOf;
		#endregion
	
		#region Explicit attribute properties
		[EntityAttribute(13, EntityAttributeState.Optional, EntityAttributeType.List, EntityAttributeType.Class, 1, -1, 15)]
		public IOptionalItemSet<CobieTypeOrComponent> @AssemblyOf 
		{ 
			get 
			{
				if(ActivationStatus != ActivationStatus.NotActivated) return _assemblyOf;
				((IPersistEntity)this).Activate(false);
				return _assemblyOf;
			} 
		}	
		#endregion



		#region Inverse attributes
		[InverseProperty("RealizingComponent")]
		[EntityAttribute(-1, EntityAttributeState.Mandatory, EntityAttributeType.Set, EntityAttributeType.Class, -1, -1, 16)]
		public IEnumerable<CobieConnection> @Connecting 
		{ 
			get 
			{
				return Model.Instances.Where<CobieConnection>(e => Equals(e.RealizingComponent), "RealizingComponent", this);
			} 
		}
		[InverseProperty("ComponentA")]
		[EntityAttribute(-1, EntityAttributeState.Mandatory, EntityAttributeType.Set, EntityAttributeType.Class, -1, -1, 17)]
		public IEnumerable<CobieConnection> @ConnectedBefore 
		{ 
			get 
			{
				return Model.Instances.Where<CobieConnection>(e => Equals(e.ComponentA), "ComponentA", this);
			} 
		}
		[InverseProperty("ComponentB")]
		[EntityAttribute(-1, EntityAttributeState.Mandatory, EntityAttributeType.Set, EntityAttributeType.Class, -1, -1, 18)]
		public IEnumerable<CobieConnection> @ConnectedAfter 
		{ 
			get 
			{
				return Model.Instances.Where<CobieConnection>(e => Equals(e.ComponentB), "ComponentB", this);
			} 
		}
		#endregion

		#region IPersist implementation
		public override void Parse(int propIndex, IPropertyValue value, int[] nestedIndex)
		{
			switch (propIndex)
			{
				case 0: 
				case 1: 
				case 2: 
				case 3: 
				case 4: 
				case 5: 
				case 6: 
				case 7: 
				case 8: 
				case 9: 
				case 10: 
				case 11: 
					base.Parse(propIndex, value, nestedIndex); 
					return;
				case 12: 
					_assemblyOf.InternalAdd((CobieTypeOrComponent)value.EntityVal);
					return;
				default:
					throw new XbimParserException(string.Format("Attribute index {0} is out of range for {1}", propIndex + 1, GetType().Name.ToUpper()));
			}
		}
		#endregion

		#region Equality comparers and operators
        public bool Equals(@CobieTypeOrComponent other)
	    {
	        return this == other;
	    }
        #endregion

		#region Custom code (will survive code regeneration)
		//## Custom code
		//##
		#endregion
	}
}