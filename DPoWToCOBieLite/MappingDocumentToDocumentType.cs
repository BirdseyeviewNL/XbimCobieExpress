﻿using System;
using System.Linq;
using Xbim.COBieLite;
using Xbim.COBieLite.CollectionTypes;
using Xbim.DPoW;

namespace XbimExchanger.DPoWToCOBieLite
{
    class MappingDocumentToDocumentType : MappingAttributableObjectToCOBieObject<Documentation, DocumentType>
    {
        protected override DocumentType Mapping(Documentation sObject, DocumentType tObject)
        {
            //perform base mapping where free attributes are converted
            base.Mapping(sObject, tObject);

            tObject.externalID = sObject.Id.ToString();
            tObject.DocumentName = sObject.Name;
            tObject.DocumentDescription = sObject.Description;
            tObject.DocumentURI = sObject.URI;


            tObject.DocumentCategory =
                Exchanger.SourceRepository.GetEncodedClassification(sObject.ClassificationReferenceIds);

            //classification and classification code encoded with ';' separator
            if (sObject.ClassificationReferenceIds != null && sObject.ClassificationReferenceIds.Any())
            {
                var reference = sObject.GetClassificationReferences(Exchanger.SourceRepository).FirstOrDefault();
                var classification =
                    Exchanger.SourceRepository.ClassificationSystems.FirstOrDefault(c => c.ClassificationReferences.Contains(reference));
                if (reference != null && classification != null)
                    tObject.DocumentCategory = String.Format("{0};{1}", classification.Name, reference.ClassificationCode);
            }


            //Issues from Jobs + Responsibilities
            if (sObject.Jobs != null && sObject.Jobs.Any())
            {
                if (tObject.DocumentIssues == null) tObject.DocumentIssues = new IssueCollectionType();
                var jMap = Exchanger.GetOrCreateMappings<MappingJobToIssueType>();
                foreach (var job in sObject.Jobs)
                {
                    var issue = jMap.GetOrCreateTargetObject(job.Id.ToString());
                    jMap.AddMapping(job, issue);
                    tObject.DocumentIssues.Add(issue);
                }
            }

            return tObject;
        }
    }
}
