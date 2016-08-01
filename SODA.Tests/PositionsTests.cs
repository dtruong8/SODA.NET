﻿using Newtonsoft.Json;
using NUnit.Framework;
using SODA.Models;
using System;

namespace SODA.Tests
{
    [TestFixture]
    [Category("Positions")]
    public class PositionsTests
    {
        [TestCase(new double[] { })]
        [TestCase(new double[] { 0.1 })]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void New_LessThan2Values_ThrowsException(double[] values)
        {
            new Positions(values);
        }

        [TestCase(new double[] { 10.11, 11.12 })]
        [TestCase(new double[] { 100.001, 111.112, 122.223 })]
        public void Positions_Serializes_ToJsonArray(double[] values)
        {
            var positions = new Positions(values);

            var expected = JsonConvert.SerializeObject(values);

            var actual = JsonConvert.SerializeObject(positions);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new double[] { 10.11, 11.12 })]
        [TestCase(new double[] { 100.001, 111.112, 122.223 })]
        public void JsonArray_Deserializes_ToPositions(double[] values)
        {
            string json = JsonConvert.SerializeObject(values);

            var positions = JsonConvert.DeserializeObject<Positions>(json);

            Assert.NotNull(positions.PositionsArray);
            Assert.AreEqual(values.Length, positions.PositionsArray.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], positions.PositionsArray[i]);
            }
        }
    }
}
