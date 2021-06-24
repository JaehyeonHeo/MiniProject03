using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRPApp.View.Setting;
using System;
using System.Linq;

namespace MRPApp.Test
{
    [TestClass]
    public class SettingsTest
    {
        // DB상에 중복된 데이터가 있는지 테스트
        [TestMethod]
        public void IsDuplicateDataTest()
        {
            var expectVal = true; // 예상값
            var inputCode = "PC010001";

            var code = Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            var realVal = code != null ? true : false;  // 실제값

            Assert.AreEqual(expectVal, realVal);
        }

        [TestMethod]
        public void IsCodeSearched()
        {
            var expectVal = 1;
            var inputCode = "설비";

            var code = Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            var realVal = code != null ? true : false;

            Assert.AreEqual(expectVal, realVal);
        }
    }
}
