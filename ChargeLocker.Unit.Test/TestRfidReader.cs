using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class TestRfidReader
    {

    }
}



namespace ECS.Test.Unit
{
    [TestFixture]
    public class EcsUnitTest
    {
        private ECS uut;
        private ITempSensor _tempSensor;
        private IHeater _heater;
        private IWindow _window;
        private int _upperThreshold = 0;
        private int _lowerThreshold = 0;

        [SetUp]
        public void Setup()
        {
            _tempSensor = NSubstitute.Substitute.For<ITempSensor>();
            _heater = NSubstitute.Substitute.For<IHeater>();
            _window = NSubstitute.Substitute.For<IWindow>();
            uut = new ECS(_tempSensor, _heater, _window, _lowerThreshold, _upperThreshold);
        }

        [TestCase(10, 15, 35)]
        [TestCase(15, 20, 35)]
        public void ECS_TempIsGreaterThanUpperThreshold(int lowerThreshold, int upperThreshold, int temp)
        {
            uut.UpperTemperatureThreshold = upperThreshold;
            uut.LowerTemperatureThreshold = lowerThreshold;
            _tempSensor.GetTemp().Returns(temp);
            uut.Regulate();
            _window.Received(0).Close();
            _window.Received(1).Open();
            _heater.Received(1).TurnOff();
            _heater.Received(0).TurnOn();
        }

        [TestCase(30, 40, 35)]
        [TestCase(35, 40, 35)]
        [TestCase(30, 35, 35)]
        public void ECS_TempIsBetweenOrEqualToThreshold(int lowerThreshold, int upperThreshold, int temp)
        {
            uut.UpperTemperatureThreshold = upperThreshold;
            uut.LowerTemperatureThreshold = lowerThreshold;
            _tempSensor.GetTemp().Returns(temp);
            uut.Regulate();
            _window.Received(1).Close();
            _window.Received(0).Open();
            _heater.Received(1).TurnOff();
            _heater.Received(0).TurnOn();
        }

        [TestCase(30, 40, 15)]
        public void ECS_TempIsLowerThanLowerThreshold(int lowerThreshold, int upperThreshold, int temp)
        {
            uut.UpperTemperatureThreshold = upperThreshold;
            uut.LowerTemperatureThreshold = lowerThreshold;
            _tempSensor.GetTemp().Returns(temp);
            uut.Regulate();
            _window.Received(1).Close();
            _window.Received(0).Open();
            _heater.Received(0).TurnOff();
            _heater.Received(1).TurnOn();
        }

        [TestCase(45, 40)]
        public void ECS_LowerThreshHoldGreaterThanUpperThreshHold_ThrowException(int lowerThreshold, int upperThreshold)
        {
            uut.UpperTemperatureThreshold = lowerThreshold + 1;
            uut.LowerTemperatureThreshold = lowerThreshold;
            Assert.Throws<ArgumentException>(() => uut.UpperTemperatureThreshold = upperThreshold);
        }

        [TestCase(50, 45)]
        public void ECS_UpperThreshHoldLowerThanLowerThreshHold_ThrowException(int lowerThreshold, int upperThreshold)
        {
            uut.UpperTemperatureThreshold = upperThreshold;                                                 //upper bliver sat til 45
            uut.LowerTemperatureThreshold = upperThreshold - 1;                                             //lower bliver sat til 44
            Assert.Throws<ArgumentException>(() => uut.LowerTemperatureThreshold = lowerThreshold);    //lower bliver sat til 50
        }
    }
}

