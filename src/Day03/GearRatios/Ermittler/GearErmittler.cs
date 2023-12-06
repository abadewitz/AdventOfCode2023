using System.ComponentModel.Design;
using GearRatios.Models;

namespace GearRatios.Ermittler;


public class GearAngrenzend
{
    public Symbol Gear { get; set; }
    public Number Partnumber { get; set; }

    public GearAngrenzend(Symbol gear, Number partnumber)
    {
        Gear = gear;
        Partnumber = partnumber;
    }
}

public class GearErmittler
{
    public List<Gear> Ermittle_Gears(EngineSchematic engineSchematic, List<Number> partnumbers)
    {
        List<Symbol> gearSymbols = engineSchematic.GetGearSymbols();

        List<Gear> result = new();
        foreach (Number nummer in partnumbers)
        {
            var auswahl = Ermittle_Gear_angrenzende_Nummern(nummer.Zeile, gearSymbols, partnumbers);
            foreach (var checkGear in auswahl.Select(li => li.Gear).Distinct().ToList())
            {
                var temp = (auswahl.Where(li => checkGear.StartPosition == li.Partnumber.StartPosition - 1
                                     || checkGear.StartPosition == li.Partnumber.EndPosition + 1
                                     || (checkGear.StartPosition >= li.Partnumber.StartPosition
                                         && checkGear.StartPosition <= li.Partnumber.EndPosition))).ToList();

                if (temp.Count == 2)
                {
                    result.Add(new Gear
                    {
                        GearSymbol = checkGear,
                        PartNumber01 = (temp.First().Partnumber),
                        PartNumber02 = (temp.Last().Partnumber)
                    });
                }
                else if(temp.Count == 1)
                {
                    
                }
                else
                {

                }
            }


            //if (auswahl.Count == 2)
            //{
            //    result.Add(new Gear
            //    {
            //        PartNumber01 = (auswahl.First().Nummer),
            //        PartNumber02 = (auswahl.Last().Nummer)
            //    });
            //}
            //else if (auswahl.Count > 2)
            //{
            //    throw new Exception("Guard");
            //}
        }

        return result.Distinct().ToList();
    }


    internal List<GearAngrenzend> Ermittle_Gear_angrenzende_Nummern(int zeile, List<Symbol> gears, List<Number> nummern)
    {
        var potenzielleGears = gears.Where(li =>
                li.Zeile == zeile
                || li.Zeile == zeile - 1
                || li.Zeile == zeile + 1)
            .ToList();

        List<GearAngrenzend> auswahl = new();

        foreach (Symbol potenzielleGear in potenzielleGears)
        {
            List<Number> potenzielleNummern = nummern.Where(li =>
                    li.Zeile == potenzielleGear.Zeile
                    || li.Zeile == potenzielleGear.Zeile - 1
                    || li.Zeile == potenzielleGear.Zeile + 1)
                .ToList();

            foreach (var potNummer in potenzielleNummern)
            {
                if ((potenzielleGear.StartPosition == potNummer.StartPosition - 1
                     || potenzielleGear.StartPosition == potNummer.EndPosition + 1
                     || (potenzielleGear.StartPosition >= potNummer.StartPosition
                         && potenzielleGear.StartPosition <= potNummer.EndPosition)))
                {
                    auswahl.Add(new GearAngrenzend(potenzielleGear, potNummer));
                }
            }
        }

        return auswahl;
    }

}