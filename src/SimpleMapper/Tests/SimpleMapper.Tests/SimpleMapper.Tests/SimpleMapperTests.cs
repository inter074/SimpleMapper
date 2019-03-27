using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMapper.Tests.TestModels;
using SimpleMapperLib.Abstractions;
using SimpleMapperLib.Mapper;
using SimpleMapperLib.Settings;

namespace SimpleMapper.Tests
{
    [TestClass]
    public class SimpleMapperTests
    {
        [TestMethod]
        public void NativeMappingTest()
        {
            var souerce = new Model1()
            {
                PublicOnlyWriteStr = "some text",
                InternalModel = new Model2(){InternalModelArray = new List<Model3>(){new Model3(){InternalModel = new Model2(){PublicStr = "it's internal" } }}},
                PublicStr = "source",
                PublicInt = 134,
                InternalModelArray = new List<Model3>() { new Model3() { PublicInt = 12, PublicStr = "from source internal array"} }
            };
            souerce.SetPublicOnlyReadStr("it's only read");

            var result1 = souerce.MapTo<Model2>();

            Assert.AreEqual(true, 
                result1.PublicStr == souerce.PublicStr && 
                result1.PublicInt == souerce.PublicInt && 
                result1.InternalModel == null && 
                result1.PublicOnlyReadStr == null && 
                result1.GetPublicOnlyWriteStr() == null);

            var result2 = souerce.MapTo<Model3>();

            Assert.AreEqual(true,
                result2.PublicStr == souerce.PublicStr &&
                result2.PublicInt == souerce.PublicInt &&
                result2.InternalModel != null &&
                result2.InternalModel.InternalModelArray.Any(x => x.PublicStr == souerce.InternalModel.InternalModelArray.FirstOrDefault()?.PublicStr));
        }

        [TestMethod]
        public void NativeMappingEachTest()
        {
            var sourceArray = new List<object>();

            var souerce1 = new Model1()
            {
                PublicOnlyWriteStr = "some text",
                InternalModel = new Model2() { InternalModelArray = new List<Model3>() { new Model3() { InternalModel = new Model2() { PublicStr = "it's internal" } } } },
                PublicStr = "source1",
                PublicInt = 134,
                InternalModelArray = new List<Model3>() { new Model3() { PublicInt = 12, PublicStr = "from source internal array" } }
            };
            souerce1.SetPublicOnlyReadStr("it's only read");

            var souerce2 = new Model3()
            {
                InternalModel = new Model2() { InternalModelArray = new List<Model3>() { new Model3() { InternalModel = new Model2() { PublicStr = "it's internal from source2" } } } },
                PublicStr = "source2",
                PublicInt = 134,
            };
            souerce1.SetPublicOnlyReadStr("it's only read");
            sourceArray.Add(souerce1);
            sourceArray.Add(souerce2);

            var result = sourceArray.MapEachTo<Model2>().ToArray();

            Assert.AreEqual(true, result.All(x => x.GetType() == typeof(Model2) && x.PublicOnlyReadStr == null && x.GetPublicOnlyWriteStr() == null) &&
                                  result.Count(x => x.PublicStr == souerce1.PublicStr) == 1 &&
                                  result.Count(x => x.PublicStr == souerce2.PublicStr) == 1 &&
                                  result.Count(x => x.InternalModelArray != null && 
                                                     x.InternalModelArray.Any(i => i.PublicStr == souerce1.InternalModelArray.FirstOrDefault()?.PublicStr)) == 1);
        }

        [TestMethod]
        public void NativeMappingEachWithSecondaryMapTest()
        {
            var sourceArray = new List<object>();

            var souerce1 = new Model1()
            {
                PublicOnlyWriteStr = "some text",
                InternalModel = new Model2() { InternalModelArray = new List<Model3>() { new Model3() { InternalModel = new Model2() { PublicStr = "it's internal" } } } },
                PublicStr = "source1",
                PublicInt = 134,
                InternalModelArray = new List<Model3>() { new Model3() { PublicInt = 12, PublicStr = "from source internal array" } }
            };
            souerce1.SetPublicOnlyReadStr("it's only read");

            var souerce2 = new Model3()
            {
                InternalModel = new Model2() { InternalModelArray = new List<Model3>() { new Model3() { InternalModel = new Model2() { PublicStr = "it's internal from source2" } } } },
                PublicStr = "source2",
                PublicInt = 134,
            };
            souerce1.SetPublicOnlyReadStr("it's only read");
            sourceArray.Add(souerce1);
            sourceArray.Add(souerce2);

            var result = sourceArray.MapEachTo<Model2>(SecondaryMapping).ToArray();

            Assert.AreEqual(true, result.All(x => x.GetType() == typeof(Model2) && x.PublicOnlyReadStr == null && x.GetPublicOnlyWriteStr() == null) &&
                                  result.Count(x => x.PublicStr == souerce1.PublicStr) == 1 &&
                                  result.Count(x => x.PublicStr == souerce2.PublicStr) == 1 &&
                                  result.Count(x => x.InternalModelArray != null &&
                                                    x.InternalModelArray.Any(i => i.PublicStr == souerce1.InternalModelArray.FirstOrDefault()?.PublicStr)) == 1 &&
                                  result.Any(x => x.ValueOfAllFilledInProperties.Contains(souerce1.PublicStr) && x.ValueOfAllFilledInProperties.Contains(souerce1.PublicStr)) &&
                                  result.Any(x => x.ValueOfAllFilledInProperties.Contains(souerce2.PublicStr) && x.ValueOfAllFilledInProperties.Contains(souerce2.PublicStr)));
        }

        [TestMethod]
        public void NativeMappingEachWithSecondaryMapWithSettingsTest()
        {
            var sourceArray = new List<object>();

            var souerce1 = new Model1()
            {
                PublicOnlyWriteStr = "some text",
                InternalModel = new Model2() { InternalModelArray = new List<Model3>() { new Model3() { InternalModel = new Model2() { PublicStr = "it's internal" } } } },
                PublicStr = "source1",
                PublicInt = 134,
                InternalModelArray = new List<Model3>() { new Model3() { PublicInt = 12, PublicStr = "from source internal array" } }
            };
            souerce1.SetPublicOnlyReadStr("it's only read");

            var souerce2 = new Model3()
            {
                InternalModel = new Model2() { InternalModelArray = new List<Model3>() { new Model3() { InternalModel = new Model2() { PublicStr = "it's internal from source2" } } } },
                PublicStr = "source2",
                PublicInt = 134,
            };
            souerce1.SetPublicOnlyReadStr("it's only read");
            sourceArray.Add(souerce1);
            sourceArray.Add(souerce2);

            var result = sourceArray.MapEachTo<Model2>(SecondaryMapping, new MapSettings() { SkipCollections = true, SkipCustomTypes = true}).ToArray();

            Assert.AreEqual(true, result.All(x => x.GetType() == typeof(Model2) && x.PublicOnlyReadStr == null && x.GetPublicOnlyWriteStr() == null) &&
                                  result.Count(x => x.PublicStr == souerce1.PublicStr) == 1 &&
                                  result.Count(x => x.PublicStr == souerce2.PublicStr) == 1 &&
                                  result.Any(x => x.ValueOfAllFilledInProperties.Contains(souerce1.PublicStr) && x.ValueOfAllFilledInProperties.Contains(souerce1.PublicStr)) &&
                                  result.Any(x => x.ValueOfAllFilledInProperties.Contains(souerce2.PublicStr) && x.ValueOfAllFilledInProperties.Contains(souerce2.PublicStr)) &&
                                  result.All(x => x.InternalModelArray == null && x.InternalModel == null));
        }

        private void SecondaryMapping(Model2 model2)
        {
            var values = model2.GetType().GetProperties()
                .Where(x => x.PropertyType.IsPrimitive || typeof(string) == x.PropertyType || x.PropertyType.GetInterface("IEnumerable") == null)
                .Select(x => new {Value = x.GetValue(model2), x.Name, x.PropertyType})
                .Where(x => x.Value != null && x.Name != nameof(model2.ValueOfAllFilledInProperties));

            foreach (var v in values)
            {
                model2.ValueOfAllFilledInProperties = model2.ValueOfAllFilledInProperties != null && model2.ValueOfAllFilledInProperties != "" 
                    ? $"{model2.ValueOfAllFilledInProperties}; {v.Name} - {v.Value}" 
                    : $"{v.Name} - {v.Value}";
            }
        }


        [TestMethod]
        public void CustomMapTest()
        {
            var p = new DestinationObjectTestProfile();
            SimpleMapperLib.Mapper.SimpleMapper.InitializeCustomMappingRules(p);
            AbstractCustomMapping.ApplyCustomMap(new Model1() {PublicInt = 1}, new Model2() {PublicInt = 2});
        }
    }

    public class DestinationObjectTestProfile : AbstractCustomMapping
    {
        public DestinationObjectTestProfile()
        {
            CreateRuleForCustomMap<Model1, Model2>()
                .Map(x => x.PublicStr, x => x.PublicStr);
        }

    }
}
