﻿using Xbim.CobieExpress;
using Xbim.Ifc4.Interfaces;

namespace Xbim.CobieExpress.Exchanger
{
    internal class MappingIfcSpatialElementToSpace : MappingIfcObjectToAsset<IIfcSpatialElement, CobieSpace>
    {
        protected override CobieSpace Mapping(IIfcSpatialElement ifcSpatialElement, CobieSpace target)
        {
            base.Mapping(ifcSpatialElement, target);

            // Over-write Description with Longname which is a better default than Description
            if (!string.IsNullOrWhiteSpace(ifcSpatialElement.LongName))
                target.Description = ifcSpatialElement.LongName;

            //use some of the attributes to fill in properties
            Helper.TrySetSimpleValue<string>("SpaceSignageName", ifcSpatialElement, s => target.RoomTag = s);
            Helper.TrySetSimpleValue<double?>("SpaceUsableHeightValue", ifcSpatialElement, f => target.UsableHeight = f);
            Helper.TrySetSimpleValue<double?>("SpaceGrossAreaValue", ifcSpatialElement, f => target.GrossArea = f);
            Helper.TrySetSimpleValue<double?>("SpaceNetAreaValue", ifcSpatialElement, f => target.NetArea = f);

            //TODO: Space Issues
            
            return target;
        }


        public override CobieSpace CreateTargetObject()
        {
            return Exchanger.TargetRepository.Instances.New<CobieSpace>();
        }
    }
}
