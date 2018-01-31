using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("lnk_GroupFeatures")]
    public class GroupFeature
    {
        public int GroupID { get; set; }
        public int FeatureID { get; set; }
        public int FunctionID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public GroupFeature()
        {
        }

        private GroupFeature(int groupId, int featureId, int functionId)
        {
            GroupID = groupId;
            FeatureID = featureId;
            FunctionID = functionId;
            CreateDate = DateTime.Now;
        }

        public static List<GroupFeature> Create(int groupId,List<int> featureIds)
        {
           var allGroupFeatures=new List<GroupFeature>();

            foreach (var featureId in featureIds)
            {
                allGroupFeatures.AddRange(Create(groupId,featureId));
            }

            return allGroupFeatures;
        }

        public static List<GroupFeature> Create(int groupId, int featureId)
        {
            //  1 -	View
            //  2 -	Update
            //  3 -	Delete
            //  4 -	Add
            //  5 -	Print

            var groupFeatures = new List<GroupFeature>
            {
                new GroupFeature(groupId, featureId, 1),
                new GroupFeature(groupId, featureId, 2),
                new GroupFeature(groupId, featureId, 3),
                new GroupFeature(groupId, featureId, 4),
                new GroupFeature(groupId, featureId, 5)
            };

         

            return groupFeatures;
        }
        public override string ToString()
        {
            return $"{GroupID} - {FeatureID} - {FunctionID}";
        }
    }
}