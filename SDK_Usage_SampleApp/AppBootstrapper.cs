﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using KasaGE;
using SDK_Usage_SampleApp.Messaging.Commands;
using SDK_Usage_SampleApp.Messaging.Events;
using SDK_Usage_SampleApp.Utils;

namespace SDK_Usage_SampleApp
{
	public class AppBootstrapper : BootstrapperBase
	{
		private CompositionContainer _container;
		private string _portName;
		private Dp25 _ecr;

		public AppBootstrapper()
		{
			Initialize();
		}

		protected override void Configure()
		{
			_container = new CompositionContainer(new AggregateCatalog(AssemblySource
					.Instance
					.Select(x => new AssemblyCatalog(x))
					.OfType<ComposablePartCatalog>()
				)
			);

			var batch = new CompositionBatch();
			
			_portName = SerialPort.GetPortNames()[0];
			_ecr = new Dp25(_portName);

			var messenger = new MessageAggregator();

			messenger.GetStream<SelectedPortChangedEvent>()
					.Subscribe(e => _ecr.ChangePort(e.PortName));



			batch.AddExportedValue<IWindowManager>(new WindowManager());
			batch.AddExportedValue<IMessageAggregator>(messenger);
			batch.AddExportedValue<Dp25>(_ecr);
			batch.AddExportedValue(_container);

			_container.Compose(batch);
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = _container.GetExportedValues<object>(contract).ToList();
			if (exports.Any())
				return exports.First();
			throw new Exception($"Could not locate any instances of contract {contract}.");
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		protected override void BuildUp(object instance)
		{
			_container.SatisfyImportsOnce(instance);
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<IShell>();
			var messenger = _container.GetExport<IMessageAggregator>();
			messenger?
				.Value?
				.Publish(new ChangeSelectedPortCommand(_portName));
		}

		protected override void OnExit(object sender, EventArgs e)
		{
			_ecr.Dispose();
		}
	}

}