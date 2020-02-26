using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker.Unit.Test
{
    [TestFixture]
    public class ChargeLockerUnitTest
    {
        private ECS _uut;
        private IHeater _heater;
        private ITempSensor _sensor;
        private IWindow _window;
        private int _lowerTemperatureThreshold = -50;
        private int _upperTemperatureThreshold = 50;

        [SetUp]
        public void Setup()
        {
            _heater = Substitute.For<IHeater>();
            _sensor = Substitute.For<ITempSensor>();
            _window = Substitute.For<IWindow>();

            _uut = new ECS(_sensor, _heater, _window, _lowerTemperatureThreshold, _upperTemperatureThreshold);
        }


        [Test]
        public void GetTemp_TempLowerThanThreshold()
        {
            _sensor.GetTemp().Returns(-51);
            _uut.Regulate();

            _heater.Received().TurnOn();
            _window.Received().Close();
        }
    }
