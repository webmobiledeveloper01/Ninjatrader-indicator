#region Using declarations
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	
	public class SoundConverterEq2 : TypeConverter
                 {

                         public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
                         {
                                 if (context == null)
                                 {
                                         return null;
                                 }
                                 ArrayList list = new ArrayList();


                                 DirectoryInfo dir = new DirectoryInfo(NinjaTrader.Core.Globals.InstallDir + "sounds");

                                 FileInfo[] files= dir.GetFiles("*.wav");

                                 foreach (FileInfo file in files)
                                 {
                                         list.Add(file.Name);
                                 }


                                 return new TypeConverter.StandardValuesCollection(list);
                         }



                         public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
                         {
                                 return true;
                         }
                 }
				 
	public class PriceEquilibrium : Indicator
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "PriceEquilibrium";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				
				
				
				SoundFiles="Alert2.wav";
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnBarUpdate()
		{
			if (CurrentBar<1) return;
			
			if (Close[0]>Open[0] && Close[1]<Open[1] && Close[1]==Open[0])
			{
				//Print(Time[0]);
				Draw.ArrowUp(this,"up"+CurrentBar.ToString(),true,0,Low[0],Brushes.Lime);
				Alert("myAlert", Priority.High, "UP", NinjaTrader.Core.Globals.InstallDir+@"\sounds\" +SoundFiles, 10, Brushes.Black, Brushes.Yellow);  
			}
			
			if (Close[0]<Open[0] && Close[1]>Open[1] && Close[1]==Open[0])
			{
				//Print(Time[0]);
				Draw.ArrowDown(this,"down"+CurrentBar.ToString(),true,0,High[0],Brushes.Red);
				Alert("myAlert", Priority.High, "DOWN", NinjaTrader.Core.Globals.InstallDir+@"\sounds\" +SoundFiles, 10, Brushes.Black, Brushes.Yellow);  
			}
		}

		#region Properties
		[Display(Name = "Alert Sound", Description = "", GroupName = "Alert", Order = 16)]
		[TypeConverter(typeof(SoundConverterEq2))]
        public string SoundFiles
        {get;set;}
		
		
		
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private PriceEquilibrium[] cachePriceEquilibrium;
		public PriceEquilibrium PriceEquilibrium()
		{
			return PriceEquilibrium(Input);
		}

		public PriceEquilibrium PriceEquilibrium(ISeries<double> input)
		{
			if (cachePriceEquilibrium != null)
				for (int idx = 0; idx < cachePriceEquilibrium.Length; idx++)
					if (cachePriceEquilibrium[idx] != null &&  cachePriceEquilibrium[idx].EqualsInput(input))
						return cachePriceEquilibrium[idx];
			return CacheIndicator<PriceEquilibrium>(new PriceEquilibrium(), input, ref cachePriceEquilibrium);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.PriceEquilibrium PriceEquilibrium()
		{
			return indicator.PriceEquilibrium(Input);
		}

		public Indicators.PriceEquilibrium PriceEquilibrium(ISeries<double> input )
		{
			return indicator.PriceEquilibrium(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.PriceEquilibrium PriceEquilibrium()
		{
			return indicator.PriceEquilibrium(Input);
		}

		public Indicators.PriceEquilibrium PriceEquilibrium(ISeries<double> input )
		{
			return indicator.PriceEquilibrium(input);
		}
	}
}

#endregion
