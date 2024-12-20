﻿using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Markdig.Helpers;

namespace Goke.Core
{
    public class Chemistry
    {

        #region chemistry

        static readonly Element[] ELEMENTS = [
            new("H", "Hydrogen", 1, 1.0079, 0.08988, 13.99, 20.271, 2.2, "1766", [1]),
            new ("He", "Helium", 2, 4.0026, 0.1786, 0.95, 4.222, null, "1868", [0]),
            new("Li", "Lithium", 3, 6.938, 0.534, 453.65, 1603, 0.98, "1817", [1]),
            new("Be", "Beryllium", 4, 9.01218, 1.85, 1560, 2742, 1.57, "1798", [2]),
            new("B", "Boron", 5, 10.806, 2.08, 2349, 4200, 2.04, "1808", [3]),
            new("C", "Carbon", 6, 12.0096, 1.821, null, null, 2.55, "3750 BC", [+4,-4]),
            new("N", "Nitrogen", 7, 14.0064, 1.251, 63.15, 77.355, 3.04, "1772", [5,-3]),
            new("O", "Oxygen", 8, 15.999, 1.429, 54.36, 90.188, 3.44, "1771", [-2]),
            new("F", "Fluorine", 9, 18.9984, 1.696, 53.48, 85.03, 3.98, "1810", [-1]),
            new("Ne", "Neon", 10, 20.1797, 0.9002, 24.56, 27.104, null, "1898", [0]),
            new("Na", "Sodium", 11, 22.9898, 0.968, 370.944, 1156.09, 0.93, "1807", [1]),
            new("Mg", "Magnesium", 12, 24.304, 1.738, 923, 1363, 1.31, "1755", [2]),
            new("Al", "Aluminium", 13, 26.9815, 2.7, 933.47, 2743, 1.61, "1825", [3]),
            new("Si", "Silicon", 14, 28.084, 2.329, 1687, 3538, 1.9, "1823", [4]),
            new("P", "Phosphorus", 15, 30.9738, 1.823, null, null, 2.19, "1669", [5,3,-3]),
            new("S", "Sulfur", 16, 32.059, 2.07, 388.36, 717.8, 2.58, "2000 BC", [6,-2]),
            new("Cl", "Chlorine", 17, 35.446, 3.2, 171.6, 239.11, 3.16, "1774", [-1]),
            new("Ar", "Argon", 18, 39.792, 1.784, 83.81, 87.302, null, "1894", [0]),
            new("K", "Potassium", 19, 39.0983, 0.862, 336.7, 1032, 0.82, "1807", [1]),
            new("Ca", "Calcium", 20, 40.0784, 1.55, 1115, 1757, 1, "1808", [2]),
            new("Sc", "Scandium", 21, 44.9559, 2.985, 1814, 3109, 1.36, "1879", [3]),
            new("Ti", "Titanium", 22, 47.8671, 4.506, 1941, 3560, 1.54, "1791", [4]),
            new("V", "Vanadium", 23, 50.9415, 6, 2183, 3680, 1.63, "1801", [5,4,3]),
            new("Cr", "Chromium", 24, 51.9962, 7.19, 2180, 2944, 1.66, "1794", [6,3,2]),
            new("Mn", "Manganese", 25, 54.938, 7.21, 1519, 2334, 1.55, "1774", [7,4,2]),
            new("Fe", "Iron", 26, 55.8452, 7.874, 1811, 3134, 1.83, "5000 BC", [3,2]),
            new("Co", "Cobalt", 27, 58.9332, 8.9, 1768, 3200, 1.88, "1735", [3,2]),
            new("Ni", "Nickel", 28, 58.6934, 8.908, 1728, 3003, 1.91, "1751", [2]),
            new("Cu", "Copper", 29, 63.5463, 8.96, 1357.77, 2835, 1.9, "9000 BC", [2,1]),
            new("Zn", "Zinc", 30, 65.382, 7.14, 692.68, 1180, 1.65, "1000 BC", [2]),
            new("Ga", "Gallium", 31, 69.7231, 5.91, 302.915, 2673, 1.81, "1875", [3]),
            new("Ge", "Germanium", 32, 72.6308, 5.323, 1211.4, 3106, 2.01, "1886", [4]),
            new("As", "Arsenic", 33, 74.9216, 5.727, null, null, 2.18, "815", [5,3]),
            new("Se", "Selenium", 34, 78.9718, 4.81, 494, 958, 2.55, "1817", [4,-2]),
            new("Br", "Bromine", 35, 79.901, 3.1028, 265.8, 332, 2.96, "1825", [-1,5]),
            new("Kr", "Krypton", 36, 83.7982, 3.749, 115.78, 119.93, 3, "1898", [0]),
            new("Rb", "Rubidium", 37, 85.4678, 1.532, 312.45, 961, 0.82, "1861", [1]),
            new("Sr", "Strontium", 38, 87.621, 2.64, 1050, 1650, 0.95, "1787", [2]),
            new("Y", "Yttrium", 39, 88.9058, 4.472, 1799, 3203, 1.22, "1794", [3]),
            new("Zr", "Zirconium", 40, 91.2242, 6.52, 2128, 4650, 1.33, "1789", [4]),
            new("Nb", "Niobium", 41, 92.9064, 8.57, 2750, 5017, 1.6, "1801", [5]),
            new("Mo", "Molybdenum", 42, 95.951, 10.28, 2896, 4912, 2.16, "1778", [6,4]),
            new("Tc", "Technetium", 43, 98.9062, 11, 2430, 4538, 1.9, "1937", [7,4]),
            new("Ru", "Ruthenium", 44, 101.072, 12.45, 2607, 4423, 2.2, "1844", [4,3]),
            new("Rh", "Rhodium", 45, 102.906, 12.41, 2237, 3968, 2.28, "1804", [3]),
            new("Pd", "Palladium", 46, 106.421, 12.023, 1828.05, 3236, 2.2, "1802", [4,2]),
            new("Ag", "Silver", 47, 107.868, 10.49, 1234.93, 2435, 1.93, "5000 BC", [1]),
            new("Cd", "Cadmium", 48, 112.414, 8.65, 594.22, 1040, 1.69, "1817", [2]),
            new("In", "Indium", 49, 114.818, 7.31, 429.749, 2345, 1.78, "1863", [3]),
            new("Sn", "Tin", 50, 118.711, 7.365, 505.08, 2875, 1.96, "3500 BC", [2,-4]),
            new("Sb", "Antimony", 51, 121.76, 6.697, 903.78, 1908, 2.05, "815", [3]),
            new("Te", "Tellurium", 52, 127.603, 6.24, 722.66, 1261, 2.1, "1782", [4]),
            new("I", "Iodine", 53, 126.904, 4.933, 386.85, 457.4, 2.66, "1811", [-1,5]),
            new("Xe", "Xenon", 54, 131.294, 5.894, 161.4, 165.051, 2.6, "1898", [0]),
            new("Cs", "Cesium", 55, 132.905, 1.93, 301.7, 944, 0.79, "1860", [1]),
            new("Ba", "Barium", 56, 137.328, 3.51, 1000, 2118, 0.89, "1772", [2]),
            new("La", "Lanthanum", 57, 138.905, 6.162, 1193, 3737, 1.1, "1838", [3]),
            new("Ce", "Cerium", 58, 140.116, 6.77, 1068, 3716, 1.12, "1803", [3]),
            new("Pr", "Praseodymium", 59, 140.908, 6.77, 1208, 3403, 1.13, "1885", [3]),
            new("Nd", "Neodymium", 60, 144.242, 7.01, 1297, 3347, 1.14, "1885", [3]),
            new("Pm", "Promethium", 61, 145, 7.26, 1315, 3273, 1.13, "1942", [3]),
            new("Sm", "Samarium", 62, 150.362, 7.52, 1345, 2173, 1.17, "1879", [3]),
            new("Eu", "Europium", 63, 151.964, 5.264, 1099, 1802, 1.2, "1896", [3]),
            new("Gd", "Gadolinium", 64, 157.253, 7.9, 1585, 3273, 1.2, "1880", [3]),
            new("Tb", "Terbium", 65, 158.925, 8.23, 1629, 3396, 1.1, "1843", [3]),
            new("Dy", "Dysprosium", 66, 162.5, 8.54, 1680, 2840, 1.22, "1886", [3]),
            new("Ho", "Holmium", 67, 164.93, 8.79, 1734, 2873, 1.23, "1878", [3]),
            new("Er", "Erbium", 68, 167.259, 9.066, 1802, 3141, 1.24, "1843", [3]),
            new("Tm", "Thulium", 69, 168.934, 9.32, 1818, 2223, 1.25, "1879", [3]),
            new("Yb", "Ytterbium", 70, 173.045, 6.9, 1097, 1469, 1.1, "1878", [3]),
            new("Lu", "Lutetium", 71, 174.967, 9.841, 1925, 3675, 1.27, "1906", [3]),
            new("Hf", "Hafnium", 72, 178.492, 13.31, 2506, 4876, 1.3, "1922", [4]),
            new("Ta", "Tantalum", 73, 180.948, 16.69, 3290, 5731, 1.5, "1802", [5]),
            new("W", "Tungsten", 74, 183.841, 19.25, 3695, 6203, 2.36, "1781", [6,4]),
            new("Re", "Rhenium", 75, 186.207, 21.02, 3459, 5869, 1.9, "1908", [5,4,3]),
            new("Os", "Osmium", 76, 190.233, 22.59, 3306, 5285, 2.2, "1803", [4]),
            new("Ir", "Iridium", 77, 192.217, 22.56, 2719, 4403, 2.2, "1803", [4,3]),
            new("Pt", "Platinum", 78, 195.085, 21.45, 2041.4, 4098, 2.28, "1735", [4,2]),
            new("Au", "Gold", 79, 196.967, 19.3, 1337.33, 3243, 2.54, "6000 BC", [3]),
            new("Hg", "Mercury", 80, 200.592, 13.534, 234.321, 629.88, 2, "1500 BC", [2,1]),
            new("Tl", "Thallium", 81, 204.382, 11.85, 577, 1746, 1.62, "1861", [3,1]),
            new("Pb", "Lead", 82, 207.21, 11.34, 600.61, 2022, 1.87, "7000 BC", [2]),
            new("Bi", "Bismuth", 83, 208.98, 9.78, 544.7, 1837, 2.02, "1000", [3]),
            new("Po", "Polonium", 84, 209, 9.196, 527, 1235, 2, "1898", [4]),
            new("At", "Astatine", 85, 210, 6.35, 575, 610, 2.2, "1940", [-1]),
            new("Rn", "Radon", 86, 222, 9.73, 202, 211.5, 2.2, "1899", [0]),
            new("Fr", "Francium", 87, 223, 1.87, 300, 950, 0.79, "1939", [1]),
            new("Ra", "Radium", 88, 226, 5.5, 1233, 2010, 0.9, "1898", [2]),
            new("Ac", "Actinium", 89, 227, 10, 1500, 3500, 1.1, "1902", [3]),
            new("Th", "Thorium", 90, 232.038, 11.724, 2023, 5061, 1.3, "1829", [4]),
            new("Pa", "Protactinium", 91, 231.036, 15.37, 1841, 4300, 1.5, "1913", [5]),
            new("U", "Uranium", 92, 238.029, 19.1, 1405.3, 4404, 1.38, "1789", [6]),
            new("Np", "Neptunium", 93, 237, 20.45, 912, 4447, 1.36, "1940", [7]),
            new("Pu", "Plutonium", 94, 244, 19.816, 912.5, 3505, 1.28, "1940", [7,4]),
            new("Am", "Americium", 95, 243, 12, 1449, 2880, 1.13, "1944", [3]),
            new("Cm", "Curium", 96, 247, 13.51, 1613, 3383, 1.28, "1944", [3]),
            new("Bk", "Berkelium", 97, 247, 14.78, 1259, 2900, 1.3, "1949", [3]),
            new("Cf", "Californium", 98, 251, 15.1, 1173, 1743, 1.3, "1950", [3]),
            new("Es", "Einsteinium", 99, 252, 8.84, 1133, 1269, 1.3, "1952", [3]),
            new("Fm", "Fermium", 100, 257, null, 1800, null, 1.3, "1952", [3]),
            new("Md", "Mendelevium", 101, 258, null, 1100, null, 1.3, "1955", [3]),
            new("No", "Nobelium", 102, 259, null, 1100, null, 1.3, "1966", [2]),
            new("Lr", "Lawrencium", 103, 262, null, 1900, null, 1.3, "1961", [3]),
            new("Rf", "Rutherfordium", 104, 261, 23.2, 2400, 5800, null, "1969", [4]),
            new("Db", "Dubnium", 105, 268, 29.3, null, null, null, "1970", []),
            new("Sg", "Seaborgium", 106, 269, 35, null, null, null, "1974", []),
            new("Bh", "Bohrium", 107, 270.133, 37.1, null, null, null, "1981", []),
            new("Hs", "Hassium", 108, 269, 40.7, 126, null, null, "1984", []),
            new("Mt", "Meitnerium", 109, 277.154, 37.4, null, null, null, "1982", []),
            new("Ds", "Darmstadtium", 110, 281, 34.8, null, null, null, "1994", []),
            new("Rg", "Roentgenium", 111, 281, 28.7, null, null, null, "1994", []),
            new("Cn", "Copernicium", 112, 285, 23.7, null, 3570, null, "1996", []),
            new("Nh", "Nihonium", 113, 286, 16, 700, 1430, null, "2003", []),
            new("Fl", "Flerovium", 114, 289, 14, 340, 420, null, "1999", []),
            new("Mc", "Moscovium", 115, 288, 13.5, 670, 1400, null, "2003", []),
            new("Lv", "Livermorium", 116, 293, 12.9, 709, 1085, null, "2000", []),
            new("Ts", "Tennessine", 117, 294, 7.17, 723, 883, null, "2009", []),
            new("Og", "Oganesson", 118, 294, 4.95, null, 350, null, "2002",[])
        ];

        /*
        static readonly Dictionary<string, Element> DIC_ELEMENTS = new()
        {
            {"H", new("H", "Hydrogen", 1, 1.0079, 0.08988, 13.99, 20.271, 2.2, "1766")},
            {"He", new("He", "Helium", 2, 4.0026, 0.1786, 0.95, 4.222, null, "1868")},
            {"Li", new("Li", "Lithium", 3, 6.938, 0.534, 453.65, 1603, 0.98, "1817")},
            {"Be", new("Be", "Beryllium", 4, 9.01218, 1.85, 1560, 2742, 1.57, "1798")},
            {"B", new("B", "Boron", 5, 10.806, 2.08, 2349, 4200, 2.04, "1808")},
            {"C", new("C", "Carbon", 6, 12.0096, 1.821, null, null, 2.55, "3750 BC")},
            {"N", new("N", "Nitrogen", 7, 14.0064, 1.251, 63.15, 77.355, 3.04, "1772")},
            {"O", new("O", "Oxygen", 8, 15.999, 1.429, 54.36, 90.188, 3.44, "1771")},
            {"F", new("F", "Fluorine", 9, 18.9984, 1.696, 53.48, 85.03, 3.98, "1810")},
            {"Ne", new("Ne", "Neon", 10, 20.1797, 0.9002, 24.56, 27.104, null, "1898")},
            {"Na", new("Na", "Sodium", 11, 22.9898, 0.968, 370.944, 1156.09, 0.93, "1807")},
            {"Mg", new("Mg", "Magnesium", 12, 24.304, 1.738, 923, 1363, 1.31, "1755")},
            {"Al", new("Al", "Aluminium", 13, 26.9815, 2.7, 933.47, 2743, 1.61, "1825")},
            {"Si", new("Si", "Silicon", 14, 28.084, 2.329, 1687, 3538, 1.9, "1823")},
            {"P", new("P", "Phosphorus", 15, 30.9738, 1.823, null, null, 2.19, "1669")},
            {"S", new("S", "Sulfur", 16, 32.059, 2.07, 388.36, 717.8, 2.58, "2000 BC")},
            {"Cl", new("Cl", "Chlorine", 17, 35.446, 3.2, 171.6, 239.11, 3.16, "1774")},
            {"Ar", new("Ar", "Argon", 18, 39.792, 1.784, 83.81, 87.302, null, "1894")},
            {"K", new("K", "Potassium", 19, 39.0983, 0.862, 336.7, 1032, 0.82, "1807")},
            {"Ca", new("Ca", "Calcium", 20, 40.0784, 1.55, 1115, 1757, 1, "1808")},
            {"Sc", new("Sc", "Scandium", 21, 44.9559, 2.985, 1814, 3109, 1.36, "1879")},
            {"Ti", new("Ti", "Titanium", 22, 47.8671, 4.506, 1941, 3560, 1.54, "1791")},
            {"V", new("V", "Vanadium", 23, 50.9415, 6, 2183, 3680, 1.63, "1801")},
            {"Cr", new("Cr", "Chromium", 24, 51.9962, 7.19, 2180, 2944, 1.66, "1794")},
            {"Mn", new("Mn", "Manganese", 25, 54.938, 7.21, 1519, 2334, 1.55, "1774")},
            {"Fe", new("Fe", "Iron", 26, 55.8452, 7.874, 1811, 3134, 1.83, "5000 BC")},
            {"Co", new("Co", "Cobalt", 27, 58.9332, 8.9, 1768, 3200, 1.88, "1735")},
            {"Ni", new("Ni", "Nickel", 28, 58.6934, 8.908, 1728, 3003, 1.91, "1751")},
            {"Cu", new("Cu", "Copper", 29, 63.5463, 8.96, 1357.77, 2835, 1.9, "9000 BC")},
            {"Zn", new("Zn", "Zinc", 30, 65.382, 7.14, 692.68, 1180, 1.65, "1000 BC")},
            {"Ga", new("Ga", "Gallium", 31, 69.7231, 5.91, 302.915, 2673, 1.81, "1875")},
            {"Ge", new("Ge", "Germanium", 32, 72.6308, 5.323, 1211.4, 3106, 2.01, "1886")},
            {"As", new("As", "Arsenic", 33, 74.9216, 5.727, null, null, 2.18, "815")},
            {"Se", new("Se", "Selenium", 34, 78.9718, 4.81, 494, 958, 2.55, "1817")},
            {"Br", new("Br", "Bromine", 35, 79.901, 3.1028, 265.8, 332, 2.96, "1825")},
            {"Kr", new("Kr", "Krypton", 36, 83.7982, 3.749, 115.78, 119.93, 3, "1898")},
            {"Rb", new("Rb", "Rubidium", 37, 85.4678, 1.532, 312.45, 961, 0.82, "1861")},
            {"Sr", new("Sr", "Strontium", 38, 87.621, 2.64, 1050, 1650, 0.95, "1787")},
            {"Y", new("Y", "Yttrium", 39, 88.9058, 4.472, 1799, 3203, 1.22, "1794")},
            {"Zr", new("Zr", "Zirconium", 40, 91.2242, 6.52, 2128, 4650, 1.33, "1789")},
            {"Nb", new("Nb", "Niobium", 41, 92.9064, 8.57, 2750, 5017, 1.6, "1801")},
            {"Mo", new("Mo", "Molybdenum", 42, 95.951, 10.28, 2896, 4912, 2.16, "1778")},
            {"Tc", new("Tc", "Technetium", 43, 98.9062, 11, 2430, 4538, 1.9, "1937")},
            {"Ru", new("Ru", "Ruthenium", 44, 101.072, 12.45, 2607, 4423, 2.2, "1844")},
            {"Rh", new("Rh", "Rhodium", 45, 102.906, 12.41, 2237, 3968, 2.28, "1804")},
            {"Pd", new("Pd", "Palladium", 46, 106.421, 12.023, 1828.05, 3236, 2.2, "1802")},
            {"Ag", new("Ag", "Silver", 47, 107.868, 10.49, 1234.93, 2435, 1.93, "5000 BC")},
            {"Cd", new("Cd", "Cadmium", 48, 112.414, 8.65, 594.22, 1040, 1.69, "1817")},
            {"In", new("In", "Indium", 49, 114.818, 7.31, 429.749, 2345, 1.78, "1863")},
            {"Sn", new("Sn", "Tin", 50, 118.711, 7.365, 505.08, 2875, 1.96, "3500 BC")},
            {"Sb", new("Sb", "Antimony", 51, 121.76, 6.697, 903.78, 1908, 2.05, "815")},
            {"Te", new("Te", "Tellurium", 52, 127.603, 6.24, 722.66, 1261, 2.1, "1782")},
            {"I", new("I", "Iodine", 53, 126.904, 4.933, 386.85, 457.4, 2.66, "1811")},
            {"Xe", new("Xe", "Xenon", 54, 131.294, 5.894, 161.4, 165.051, 2.6, "1898")},
            {"Cs", new("Cs", "Cesium", 55, 132.905, 1.93, 301.7, 944, 0.79, "1860")},
            {"Ba", new("Ba", "Barium", 56, 137.328, 3.51, 1000, 2118, 0.89, "1772")},
            {"La", new("La", "Lanthanum", 57, 138.905, 6.162, 1193, 3737, 1.1, "1838")},
            {"Ce", new("Ce", "Cerium", 58, 140.116, 6.77, 1068, 3716, 1.12, "1803")},
            {"Pr", new("Pr", "Praseodymium", 59, 140.908, 6.77, 1208, 3403, 1.13, "1885")},
            {"Nd", new("Nd", "Neodymium", 60, 144.242, 7.01, 1297, 3347, 1.14, "1885")},
            {"Pm", new("Pm", "Promethium", 61, 145, 7.26, 1315, 3273, 1.13, "1942")},
            {"Sm", new("Sm", "Samarium", 62, 150.362, 7.52, 1345, 2173, 1.17, "1879")},
            {"Eu", new("Eu", "Europium", 63, 151.964, 5.264, 1099, 1802, 1.2, "1896")},
            {"Gd", new("Gd", "Gadolinium", 64, 157.253, 7.9, 1585, 3273, 1.2, "1880")},
            {"Tb", new("Tb", "Terbium", 65, 158.925, 8.23, 1629, 3396, 1.1, "1843")},
            {"Dy", new("Dy", "Dysprosium", 66, 162.5, 8.54, 1680, 2840, 1.22, "1886")},
            {"Ho", new("Ho", "Holmium", 67, 164.93, 8.79, 1734, 2873, 1.23, "1878")},
            {"Er", new("Er", "Erbium", 68, 167.259, 9.066, 1802, 3141, 1.24, "1843")},
            {"Tm", new("Tm", "Thulium", 69, 168.934, 9.32, 1818, 2223, 1.25, "1879")},
            {"Yb", new("Yb", "Ytterbium", 70, 173.045, 6.9, 1097, 1469, 1.1, "1878")},
            {"Lu", new("Lu", "Lutetium", 71, 174.967, 9.841, 1925, 3675, 1.27, "1906")},
            {"Hf", new("Hf", "Hafnium", 72, 178.492, 13.31, 2506, 4876, 1.3, "1922")},
            {"Ta", new("Ta", "Tantalum", 73, 180.948, 16.69, 3290, 5731, 1.5, "1802")},
            {"W", new("W", "Tungsten", 74, 183.841, 19.25, 3695, 6203, 2.36, "1781")},
            {"Re", new("Re", "Rhenium", 75, 186.207, 21.02, 3459, 5869, 1.9, "1908")},
            {"Os", new("Os", "Osmium", 76, 190.233, 22.59, 3306, 5285, 2.2, "1803")},
            {"Ir", new("Ir", "Iridium", 77, 192.217, 22.56, 2719, 4403, 2.2, "1803")},
            {"Pt", new("Pt", "Platinum", 78, 195.085, 21.45, 2041.4, 4098, 2.28, "1735")},
            {"Au", new("Au", "Gold", 79, 196.967, 19.3, 1337.33, 3243, 2.54, "6000 BC")},
            {"Hg", new("Hg", "Mercury", 80, 200.592, 13.534, 234.321, 629.88, 2, "1500 BC")},
            {"Tl", new("Tl", "Thallium", 81, 204.382, 11.85, 577, 1746, 1.62, "1861")},
            {"Pb", new("Pb", "Lead", 82, 207.21, 11.34, 600.61, 2022, 1.87, "7000 BC")},
            {"Bi", new("Bi", "Bismuth", 83, 208.98, 9.78, 544.7, 1837, 2.02, "1000")},
            {"Po", new("Po", "Polonium", 84, 209, 9.196, 527, 1235, 2, "1898")},
            {"At", new("At", "Astatine", 85, 210, 6.35, 575, 610, 2.2, "1940")},
            {"Rn", new("Rn", "Radon", 86, 222, 9.73, 202, 211.5, 2.2, "1899")},
            {"Fr", new("Fr", "Francium", 87, 223, 1.87, 300, 950, 0.79, "1939")},
            {"Ra", new("Ra", "Radium", 88, 226, 5.5, 1233, 2010, 0.9, "1898")},
            {"Ac", new("Ac", "Actinium", 89, 227, 10, 1500, 3500, 1.1, "1902")},
            {"Th", new("Th", "Thorium", 90, 232.038, 11.724, 2023, 5061, 1.3, "1829")},
            {"Pa", new("Pa", "Protactinium", 91, 231.036, 15.37, 1841, 4300, 1.5, "1913")},
            {"U", new("U", "Uranium", 92, 238.029, 19.1, 1405.3, 4404, 1.38, "1789")},
            {"Np", new("Np", "Neptunium", 93, 237, 20.45, 912, 4447, 1.36, "1940")},
            {"Pu", new("Pu", "Plutonium", 94, 244, 19.816, 912.5, 3505, 1.28, "1940")},
            {"Am", new("Am", "Americium", 95, 243, 12, 1449, 2880, 1.13, "1944")},
            {"Cm", new("Cm", "Curium", 96, 247, 13.51, 1613, 3383, 1.28, "1944")},
            {"Bk", new("Bk", "Berkelium", 97, 247, 14.78, 1259, 2900, 1.3, "1949")},
            {"Cf", new("Cf", "Californium", 98, 251, 15.1, 1173, 1743, 1.3, "1950")},
            {"Es", new("Es", "Einsteinium", 99, 252, 8.84, 1133, 1269, 1.3, "1952")},
            {"Fm", new("Fm", "Fermium", 100, 257, null, 1800, null, 1.3, "1952")},
            {"Md", new("Md", "Mendelevium", 101, 258, null, 1100, null, 1.3, "1955")},
            {"No", new("No", "Nobelium", 102, 259, null, 1100, null, 1.3, "1966")},
            {"Lr", new("Lr", "Lawrencium", 103, 262, null, 1900, null, 1.3, "1961")},
            {"Rf", new("Rf", "Rutherfordium", 104, 261, 23.2, 2400, 5800, null, "1969")},
            {"Db", new("Db", "Dubnium", 105, 268, 29.3, null, null, null, "1970")},
            {"Sg", new("Sg", "Seaborgium", 106, 269, 35, null, null, null, "1974")},
            {"Bh", new("Bh", "Bohrium", 107, 270.133, 37.1, null, null, null, "1981")},
            {"Hs", new("Hs", "Hassium", 108, 269, 40.7, 126, null, null, "1984")},
            {"Mt", new("Mt", "Meitnerium", 109, 277.154, 37.4, null, null, null, "1982")},
            {"Ds", new("Ds", "Darmstadtium", 110, 281, 34.8, null, null, null, "1994")},
            {"Rg", new("Rg", "Roentgenium", 111, 281, 28.7, null, null, null, "1994")},
            {"Cn", new("Cn", "Copernicium", 112, 285, 23.7, null, 3570, null, "1996")},
            {"Nh", new("Nh", "Nihonium", 113, 286, 16, 700, 1430, null, "2003")},
            {"Fl", new("Fl", "Flerovium", 114, 289, 14, 340, 420, null, "1999")},
            {"Mc", new("Mc", "Moscovium", 115, 288, 13.5, 670, 1400, null, "2003")},
            {"Lv", new("Lv", "Livermorium", 116, 293, 12.9, 709, 1085, null, "2000")},
            {"Ts", new("Ts", "Tennessine", 117, 294, 7.17, 723, 883, null, "2009")},
            {"Og", new("Og", "Oganesson", 118, 294, 4.95, null, 350, null, "2002")}
        };
        */

        static int[] nobleGases = [2, 10, 18, 36, 54, 86, 118];
        static int[] nonMetal = [6, 7, 8, 15, 16, 34];
        static int[] basicMetal = [13, 31, 49, 50, 81, 82, 83, 113, 114, 115, 116];
        static int[] semiMetal = [5, 14, 32, 33, 51, 52, 84];
        static int[] alkaliMetal = [3, 11, 19, 37, 55, 87];
        static int[] alkineEarth = alkaliMetal.Select(s => s + 1).ToArray(); // [4,12,20,38,56,88];
        static int[] group3 = [5, 13, 31, 49, 81, 113];
        static int[] group4 = group3.Select(s => s + 1).ToArray(); // [6,14,32,50,82,114];
        static int[] group5 = group3.Select(s => s + 2).ToArray(); // [7,15,33,51,83,115];
        static int[] group6 = group3.Select(s => s + 3).ToArray(); // [8,16,34,52,84,116];

        static int[] halogens = [9, 17, 35, 53, 85, 117];

        public static IEnumerable<Element> AlkaliMetals => ELEMENTS.Where(w => alkaliMetal.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> AlkineEarths => ELEMENTS.Where(w => alkineEarth.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> NobleGases => ELEMENTS.Where(w => nobleGases.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> NonMetals => ELEMENTS.Where(w => nonMetal.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> BasicMetals => ELEMENTS.Where(w => basicMetal.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> SemiMetals => ELEMENTS.Where(w => semiMetal.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> Halogens => ELEMENTS.Where(w => halogens.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> TransitionMetals => ELEMENTS.Where(w =>
                (21 <= w.AtomicNumber && w.AtomicNumber <= 30) ||
                (39 <= w.AtomicNumber && w.AtomicNumber <= 48) ||
                (72 <= w.AtomicNumber && w.AtomicNumber <= 80) ||
                (104 <= w.AtomicNumber && w.AtomicNumber <= 112)
        );
        public static IEnumerable<Element> Actinides => ELEMENTS.Where(w => 89 <= w.AtomicNumber && w.AtomicNumber <= 103);
        public static IEnumerable<Element> Lanthanides => ELEMENTS.Where(w => 57 <= w.AtomicNumber && w.AtomicNumber <= 71);
        public static IEnumerable<Element> Elements1To20 => ELEMENTS.Where(w => w.AtomicNumber <= 20);
        public static IEnumerable<Element> AllElements => ELEMENTS;

        public static IEnumerable<Element> Group1 => ELEMENTS.Where(w => w.AtomicNumber == 1).Union(AlkaliMetals);
        public static IEnumerable<Element> Group2 => AlkineEarths;
        public static IEnumerable<Element> Group3 => ELEMENTS.Where(w => group3.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> Group4 => ELEMENTS.Where(w => group4.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> Group5 => ELEMENTS.Where(w => group5.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> Group6 => ELEMENTS.Where(w => group6.Any(a => a == w.AtomicNumber));
        public static IEnumerable<Element> Group7 => Halogens;
        public static IEnumerable<Element> Group8 => NobleGases;

        public static Element? GetElementBySymbol(string symbol)
        {
            return ELEMENTS.FirstOrDefault(f => string.Compare(f.Symbol, symbol, true) == 0);
        }

        public static Element? GetElementByName(string name)
        {
            return ELEMENTS.FirstOrDefault(f => string.Compare(f.Name, name, true) == 0);
        }

        public static double MolarMass(Dictionary<string, double> compound)
        {
            double mass = 0;

            foreach (var c in compound)
            {
                var element = GetElementBySymbol(c.Key);
                if (element == null)
                {
                    throw new ArgumentOutOfRangeException($"{c.Key} does not exist");
                }

                mass += element.AtomicWeight * c.Value;
            }

            return mass;
        }

        public static double MolarMass(string compound)
        {
            var dicCompound = ParseCompound(compound);
            return MolarMass(dicCompound);
        }

        public static double OxidationState(string compound)
        {
            double result = 0;
            var dicCompound = ParseCompound(compound);

            foreach (var c in dicCompound)
            {
                var k = ElementOxidationNumber(c.Key);

                result += k * c.Value;
            }

            return result;
        }

        private static int ElementOxidationNumber(string elementSymbol)
        {
            switch (elementSymbol)
            {
                case string s when Group1.Select(s => s.Symbol).Any(a => a == s):
                    return 1;
                case string s when Group2.Select(s => s.Symbol).Any(a => a == s):
                    return 2;
                case string s when Group3.Select(s => s.Symbol).Any(a => a == s):
                    return 3;
                case string s when Group4.Select(s => s.Symbol).Any(a => a == s):
                    return 4;
                case string s when Group5.Select(s => s.Symbol).Any(a => a == s):
                    return -3;
                case string s when Group6.Select(s => s.Symbol).Any(a => a == s):
                    return -2;
                case string s when Group7.Select(s => s.Symbol).Any(a => a == s):
                    return -1;
                case string s when Group8.Select(s => s.Symbol).Any(a => a == s):
                // return 0;
                default:
                    return 0;
            }
        }

        public static Dictionary<string, double> ParseCompound(string compound)
        {
            char last = ' ';
            StringBuilder alph = new();
            StringBuilder num = new();

            Dictionary<string, double> dic = new Dictionary<string, double>();
            foreach (var c in compound)
            {
                if (c.IsAlpha())
                {
                    if (last.IsDigit() || (!last.IsAlphaUpper() && c.IsAlphaUpper()) || (c.IsAlphaUpper()))
                    {
                        // Console.WriteLine();
                        if (alph.Length > 0)
                        {
                            dic[alph.ToString()] = double.Parse(num.Length > 0 ? num.ToString() : "1");
                        }
                        alph.Clear();
                        num.Clear();
                    }
                    // Console.Write(c);
                    alph.Append(c);
                }

                if (c.IsDigit() || c == '.')
                {
                    if (!last.IsDigit())
                    {
                        // Console.WriteLine();
                    }
                    // Console.Write(c);
                    num.Append(c);
                }

                last = c;
            }
            if (alph.Length > 0)
            {
                dic[alph.ToString()] = double.Parse(num.Length > 0 ? num.ToString() : "1");
            }
            return dic;
        }
        #endregion

        #region ions

        static readonly List<Cation> Cations = new()
        {
            new("Na", "Sodium ion", null,1, TestColor:"Brilliant or Golden Yellow"),
            new("K", "Potassium ion", null,1, "Lilac or Violet"),
            new("Ca", "Calcium ion", null,2, "Brick Red or Orange-red"),
            new("Cu", "Copper ion", null,2, "Bluish-Green"),
            new("Pb", "Lead ion", null,2, "Pale-Blue"),
            new("Ba", "Barium ion", null,2, "Light yellowish-green or Apple-green"),
        };

        static readonly List<Anion> Anions = new()
        {
            new ("CO3", "ion",null,1,"White deposit"),
            new ("SO3", "ion",null,1,"No deposit"),
            new ("SO4", "ion",null,1,"White precipitate"),
            new ("S", "ion",null,1,"Black Precipitate"),
            new ("Cl", "ion",null,1,"White precipitate"),
            new ("Br", "ion",null,1,"Creamy or Yellowish precipitate"),
            new ("I", "ion",null,1,"Pale-Yellow precipitate"),
            // , SO3, SO4, S, Cl, Br, I
        };


        #endregion


        public record Element(
            string Symbol,
            string Name,
            int AtomicNumber,
            double AtomicWeight,
            double? Density,
            double? MeltingPoint,
            double? BoilingPoint,
            double? Electronegativity,
            string? Discovered,
            int[]? InonicCharge
        );

        public record Chemical(
            string? Formula,
            string? Name,
            string? OtherName
        );

        public record Cation(
            string? Formula,
            string? Name,
            string? OtherName,
            double? OxidationNumber,
            string TestColor
            ) : Chemical(Formula, Name, OtherName);

        public record Anion(
            string? Formula,
            string? Name,
            string? OtherName,
            double? OxidationNumber,
            string TestColor
            ) : Chemical(Formula, Name, OtherName);

    }
}
