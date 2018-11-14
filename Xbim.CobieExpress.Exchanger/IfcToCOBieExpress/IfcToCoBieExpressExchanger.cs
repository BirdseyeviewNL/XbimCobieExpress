﻿using System.Collections.Generic;
using System.Linq;
using Xbim.CobieExpress;
using Xbim.Common;
using Xbim.CobieExpress.Exchanger.FilterHelper;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using XbimExchanger.IfcToCOBieExpress.Classifications;
using XbimExchanger.IfcHelpers;
using Microsoft.Extensions.Logging;

namespace XbimExchanger.IfcToCOBieExpress
{
    public class IfcToCoBieExpressExchanger : XbimExchanger<IModel, IModel>
    {
        private readonly bool _classify;
        internal COBieExpressHelper Helper ;
        /// <summary>
        /// Instantiates a new IIfcToCOBieLiteUkExchanger class.
        /// </summary>
        public IfcToCoBieExpressExchanger(IModel source, IModel target, ILogger logger, ReportProgressDelegate reportProgress = null, OutPutFilters filter = null, string configFile = null, EntityIdentifierMode extId = EntityIdentifierMode.IfcEntityLabels, SystemExtractionMode sysMode = SystemExtractionMode.System | SystemExtractionMode.Types, bool classify = false) 
            : base(source, target)
        {
            ReportProgress.Progress = reportProgress; //set reporter
            Helper = new COBieExpressHelper(this, ReportProgress, logger, filter, configFile, extId, sysMode);
            Helper.Init();

            _classify = classify;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IModel Convert()
        {
            var mapping = GetOrCreateMappings<MappingIfcBuildingToFacility>();
            var classifier = new Classifier(this);
            var buildings = SourceRepository.Instances.OfType<IIfcBuilding>().ToList();
            var facilities = new List<CobieFacility>(buildings.Count);
            foreach (var building in buildings)
            {
                var facility = TargetRepository.Instances.New<CobieFacility>();
                facility = mapping.AddMapping(building, facility);
                if(_classify)
                    classifier.Classify(facility);
                facilities.Add(facility);
            }
            return TargetRepository;
        }
    }
}
