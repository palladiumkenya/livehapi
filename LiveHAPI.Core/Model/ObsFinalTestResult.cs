using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ObsFinalTestResult : Entity<Guid>
    {
        
        public Guid? FirstTestResult { get; set; }
        public string FirstTestResultCode { get; set; }
        
        public Guid? SecondTestResult { get; set; }
        public string SecondTestResultCode { get; set; }
        
        public Guid? FinalResult { get; set; }
        public string FinalResultCode { get; set; }
        
        public Guid? ResultGiven { get; set; }
        
        public Guid? CoupleDiscordant { get; set; }
        
        public Guid? SelfTestOption { get; set; }
        
        public Guid EncounterId { get; set; }


        public ObsFinalTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

        private ObsFinalTestResult(Guid id, Guid? firstTestResult, Guid? secondTestResult, Guid? endResult,
            Guid encounterId)
        {
            Id = id;
            FirstTestResult = firstTestResult;
            SecondTestResult = secondTestResult;
            FinalResult = endResult;
            EncounterId = encounterId;
        }

        private ObsFinalTestResult(Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid encounterId) :
            this(LiveGuid.NewGuid(), firstTestResult, secondTestResult, endResult, encounterId)
        {

        }

        public static ObsFinalTestResult Create(Guid id, Guid? firstTestResult, Guid? secondTestResult, Guid? endResult,
            Guid encounterId)
        {
            return new ObsFinalTestResult(id, firstTestResult, secondTestResult, endResult, encounterId);
        }

        public static ObsFinalTestResult Create(Guid? firstTestResult, Guid? secondTestResult, Guid? endResult,
            Guid encounterId)
        {
            return new ObsFinalTestResult(firstTestResult, secondTestResult, endResult, encounterId);
        }

        public static ObsFinalTestResult CreateFirst(Guid? firstTestResult, Guid encounterId)
        {
            return new ObsFinalTestResult(firstTestResult, null, null, encounterId);
        }

        public void UpdateSetFirstResult(Guid? result)
        {
            FirstTestResult = result.IsNullOrEmpty() ? null : result;
        }

        public void UpdateSetSecondResult(Guid? result)
        {
            SecondTestResult = result.IsNullOrEmpty() ? null : result;
        }

        public void UpdateSetEndResult(Guid? result)
        {
            FinalResult = result.IsNullOrEmpty() ? null : result;
        }

        public void ProcessEndResult(List<CategoryItem> _categoryItems)
        {
            var neg = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "N".ToLower());
            var pos = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "P".ToLower());
            var inv = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "I".ToLower());
            var ic = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "IC".ToLower());


            // NO First Result

            if (FirstTestResult.IsNullOrEmpty())
            {
                UpdateSetEndResult(Guid.Empty);
                return;
            }

            // NEG First Result

            if (
                !FirstTestResult.IsNullOrEmpty() && null != neg &&
                FirstTestResult == neg.ItemId)
            {
                UpdateSetSecondResult(Guid.Empty);
                UpdateSetEndResult(neg.ItemId);
                return;
            }


            // Pos | NUll > NULL

            if (
                !FirstTestResult.IsNullOrEmpty() && null != pos && FirstTestResult == pos.ItemId &&
                SecondTestResult.IsNullOrEmpty()
            )
            {
                UpdateSetSecondResult(Guid.Empty);
                UpdateSetEndResult(Guid.Empty);
                return;
            }

            // null | Pos|Neg > NULL

            if (
                FirstTestResult.IsNullOrEmpty() && !SecondTestResult.IsNullOrEmpty()
            )
            {
                UpdateSetEndResult(Guid.Empty);
                return;
            }

            // Pos | Pos > Pos

            if (
                !FirstTestResult.IsNullOrEmpty() && null != pos && FirstTestResult == pos.ItemId &&
                !SecondTestResult.IsNullOrEmpty() && SecondTestResult == pos.ItemId
            )
            {
                UpdateSetEndResult(pos.ItemId);
                return;
            }

            // Pos | Neg > Ic

            if (
                !FirstTestResult.IsNullOrEmpty() && null != pos && FirstTestResult == pos.ItemId &&
                !SecondTestResult.IsNullOrEmpty() && null != neg && SecondTestResult == neg.ItemId
            )
            {
                if (null != ic)
                {
                    UpdateSetEndResult(ic.ItemId);
                }
                return;
            }

        }
    }
}