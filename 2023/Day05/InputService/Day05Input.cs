﻿using AdventOfCode._2023.Day05.DayFiveLogger;
using System.Numerics;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Collections.Generic;
using AdventOfCode._2023.Day05.SeedRangeStructures;

namespace AdventOfCode._2023.Day05.InputService.DayFiveInput
{
    public class Day05Input
    {
        private readonly Day05Logger _logger;

        public string Seeds { get; } = "4106085912 135215567 529248892 159537194 1281459911 114322341 1857095529 814584370 2999858074 50388481 3362084117 37744902 3471634344 240133599 3737494864 346615684 1585884643 142273098 917169654 286257440";

        public List<BigInteger> ParsedSeeds = new List<BigInteger>();

        public SortedDictionary<MapType, string> AllMaps { get; } = new SortedDictionary<MapType, string>
        {
            [MapType.SeedToSoil] =
                "1640984363 3136305987 77225710\n" +
                "3469528922 1857474741 56096642\n" +
                "278465165 2901870617 105516220\n" +
                "1442950910 1913571383 198033453\n" +
                "463085535 1458252975 13696838\n" +
                "1718210073 1686050230 171424511\n" +
                "383981385 3064707638 71598349\n" +
                "1267048154 3759577328 175902756\n" +
                "3262934306 1479455614 206594616\n" +
                "2493001016 200414015 157177749\n" +
                "3885112776 411057950 157348766\n" +
                "4042461542 1181162257 199039568\n" +
                "476782373 2111604836 790265781\n" +
                "455579734 1471949813 7505801\n" +
                "1889634584 3213531697 546045631\n" +
                "4241501110 357591764 53466186\n" +
                "3525625564 3935480084 359487212\n" +
                "2650178765 568406716 612755541\n" +
                "200414015 1380201825 78051150\n" +
                "2435680215 3007386837 57320801",

            [MapType.SoilToFertilizer] =
                "513693437 1166448955 406316429\n" +
                "3977989337 1831898517 148449061\n" +
                "2857616419 1749713256 55966993\n" +
                "2245899978 767737007 398711948\n" +
                "3462028551 3402487827 258322330\n" +
                "1207747701 2246116788 274586148\n" +
                "1857449833 2520702936 106524473\n" +
                "950443356 290304833 224196260\n" +
                "2913583412 1805680249 26218268\n" +
                "290304833 3849361293 119346889\n" +
                "3720350881 3244527156 157960671\n" +
                "920009866 1634093064 30433490\n" +
                "409651722 2627227409 104041715\n" +
                "3916661657 1572765384 61327680\n" +
                "1624788708 2115889182 130227606\n" +
                "1963974306 3695907849 9066861\n" +
                "1755016314 1980347578 102433519\n" +
                "1973041167 3151152620 93374536\n" +
                "1482333849 710468850 57268157\n" +
                "2644611926 2899798022 213004493\n" +
                "1539602006 1664526554 85186702\n" +
                "2210802286 3660810157 35097692\n" +
                "3135769437 3968708182 326259114\n" +
                "1174639616 2082781097 33108085\n" +
                "3878311552 3112802515 38350105\n" +
                "2066415703 3704974710 144386583\n" +
                "2939801680 514501093 195967757\n" +
                "4126438398 2731269124 168528898",

            [MapType.FertilizerToWater] =
                "1274667249 2789153677 35498097\n" +
                "1119124697 1423189114 4201717\n" +
                "1775973674 127038866 409949870\n" +
                "2636872711 677641697 238584014\n" +
                "998550357 2708616519 80537158\n" +
                "1613168083 3037802277 162805591\n" +
                "1123326414 2618446916 90169603\n" +
                "2516959328 2034731526 119913383\n" +
                "3879305887 3993605604 147788774\n" +
                "1213496017 2616229293 2217623\n" +
                "3412445194 3202949622 62545934\n" +
                "2875456725 2194682091 362593593\n" +
                "1079087515 2154644909 40037182\n" +
                "2468026227 3426058027 48933101\n" +
                "0 1427390831 607340695\n" +
                "2185923544 0 127038866\n" +
                "1310165346 916225711 76820908\n" +
                "2312962410 993046619 155063817\n" +
                "607340695 1369464790 53724324\n" +
                "1389328008 536988736 140652961\n" +
                "1386986254 3200607868 2341754\n" +
                "4241108893 3701026057 53858403\n" +
                "661065019 2824651774 213150503\n" +
                "1529980969 3342870913 83187114\n" +
                "921175000 3265495556 77375357\n" +
                "874215522 1148110436 46959478\n" +
                "4180667579 3914761609 60441314\n" +
                "3238050318 1195069914 174394876\n" +
                "3701026057 3754884460 159877149\n" +
                "4027094661 4141394378 153572918\n" +
                "3860903206 3975202923 18402681\n" +
                "1215713640 2557275684 58953609",

            [MapType.WaterToLight] =
                "3346671099 2139469351 253535694\n" +
                "3600206793 4187771498 107195798\n" +
                "1271601308 936374322 163567625\n" +
                "3890528820 1799438963 144160054\n" +
                "1731948725 3256631615 148580525\n" +
                "3859991790 2573461247 250171\n" +
                "389304550 1099941947 124474859\n" +
                "2322259245 1445947039 39679535\n" +
                "1544278612 1943599017 46211136\n" +
                "1124738947 789511961 146862361\n" +
                "3860946526 2109887057 29582294\n" +
                "891137223 173339870 233601724\n" +
                "3044858977 1485626574 301812122\n" +
                "627354019 0 173339870\n" +
                "800693889 1224416806 90443334\n" +
                "4046689141 2034661517 75225540\n" +
                "4166766045 4059570247 128201251\n" +
                "0 1314860140 120308793\n" +
                "4034688874 1787438696 12000267\n" +
                "2294392242 2393005045 27867003\n" +
                "2361938780 2573711418 682920197\n" +
                "4121914681 1989810153 44851364\n" +
                "1674653431 3705039670 14123300\n" +
                "1445242474 3719162970 99036138\n" +
                "513779409 406941594 113574610\n" +
                "3707402591 2420872048 152589199\n" +
                "2053021103 3818199108 241371139\n" +
                "3860241961 1445242474 704565\n" +
                "1880529250 3489375823 172491853\n" +
                "1688776731 3661867676 43171994\n" +
                "120308793 520516204 268995757\n" +
                "1590489748 3405212140 84163683",



            [MapType.LightToTemperature] =
                "1711282888 1572780528 87721767\n" +
                "154126417 0 43277112\n" +
                "950353983 1343526858 179094373\n" +
                "2607445049 2714989532 110883165\n" +
                "197403529 400138402 104876963\n" +
                "302280492 43277112 202734873\n" +
                "2990325458 1942480091 22763517\n" +
                "2203652414 1550069916 22710612\n" +
                "3347561974 4075093920 130901113\n" +
                "1328424170 3514387681 17133869\n" +
                "3187047160 4220120429 74846867\n" +
                "3555672228 4205995033 676520\n" +
                "555737175 2292503071 178861674\n" +
                "936905107 4206671553 13448876\n" +
                "2718328214 2825872697 25755942\n" +
                "2042629442 945726244 118907923\n" +
                "2161537365 3027858084 42115049\n" +
                "1657501024 1888698227 53781864\n" +
                "2943656210 3804007444 46669248\n" +
                "2226363026 2004226072 108596129\n" +
                "0 246011985 154126417\n" +
                "734598849 2928837780 99020304\n" +
                "833619153 1660502295 103285954\n" +
                "2744084156 3436875901 77511780\n" +
                "1299692440 2112822201 28731730\n" +
                "2821595936 782812975 122060274\n" +
                "1799004655 2471364745 95185072\n" +
                "3082783503 1965243608 38982464\n" +
                "2334959155 3531521550 272485894\n" +
                "3121765967 2227221878 65281193\n" +
                "1392230801 3850676692 224417228\n" +
                "3013088975 528288490 69694528\n" +
                "1616648029 904873249 40852995\n" +
                "1345558039 3069973133 46672762\n" +
                "3681258726 1064634167 278892691\n" +
                "1894189727 2566549817 148439715\n" +
                "1129448356 597983018 170244084\n" +
                "3261894027 2141553931 85667947\n" +
                "3478463087 2851628639 77209141\n" +
                "3556348748 1763788249 124909978\n" +
                "4280381423 768227102 14585873\n" +
                "528288490 1522621231 27448685\n" +
                "3960151417 3116645895 320230006",

        [MapType.TemperatureToHumidity] =
            "2401309547 2063893326 5931150\n" +
            "4081820678 1536756293 195703517\n" +
            "3389837279 4114880485 97950323\n" +
            "67647704 537880870 95615044\n" +
            "3487787602 2069824476 16209316\n" +
            "212366581 0 210367924\n" +
            "0 470233166 67647704\n" +
            "163262748 331958016 49103833\n" +
            "3921228390 3754997923 26024946\n" +
            "1883070873 986328296 29590327\n" +
            "1844673227 4256569650 38397646\n" +
            "422734505 381061849 89171317\n" +
            "3835004067 4212830808 43738842\n" +
            "1753523088 1732459810 63479835\n" +
            "4277524195 3781022869 17443101\n" +
            "926010074 3798465970 316414515\n" +
            "511905822 210367924 31838014\n" +
            "3565158997 3485152853 269845070\n" +
            "3878742909 776643827 42485481\n" +
            "3352296690 1795939645 37540589\n" +
            "2877358153 2512426556 474938537\n" +
            "1419995251 1068661114 304321846\n" +
            "1348461498 705110074 71533753\n" +
            "1724317097 1372982960 29205991\n" +
            "1974916783 2086033792 426392764\n" +
            "2407240697 3015035397 470117456\n" +
            "1242424589 880291387 106036909\n" +
            "642854491 1833480234 230413092\n" +
            "3503996918 819129308 61162079\n" +
            "543743836 242205938 89752078\n" +
            "873267583 1015918623 52742491\n" +
            "1817002923 2987365093 27670304\n" +
            "1912661200 642854491 62255583\n" +
            "3947253336 1402188951 134567342",

        [MapType.HumidityToLocation] =
            "2955816171 2260659770 927037009\n" +
            "1906648752 2188942242 71717528\n" +
            "848878920 35928575 8026852\n" +
            "4100692468 1994667414 194274828\n" +
            "2066384942 3405536067 889431229\n" +
            "559945395 1052613350 288933525\n" +
            "3882853180 3187696779 217839288\n" +
            "856905772 1341546875 164300625\n" +
            "0 528596530 524016820\n" +
            "1978366280 1723924810 88018662\n" +
            "1044385850 67134880 400987760\n" +
            "524016820 0 35928575\n" +
            "1021206397 43955427 23179453\n" +
            "1445373610 468122640 60473890\n" +
            "1723924810 1811943472 182723942"



        };

        public SortedDictionary<MapType, string> QuestionTestMaps { get; } = new SortedDictionary<MapType, string>
        {
            [MapType.SeedToSoil] =
        "50 98 2\n" +
        "52 50 48",

            [MapType.SoilToFertilizer] =
        "0 15 37\n" +
        "37 52 2\n" +
        "39 0 15",

            [MapType.FertilizerToWater] =
        "49 53 8\n" +
        "0 11 42\n" +
        "42 0 7\n" +
        "57 7 4",

            [MapType.WaterToLight] =
        "88 18 7\n" +
        "18 25 70",

            [MapType.LightToTemperature] =
        "45 77 23\n" +
        "81 45 19\n" +
        "68 64 13",

            [MapType.TemperatureToHumidity] =
        "0 69 1\n" +
        "1 0 69",

            [MapType.HumidityToLocation] =
        "60 56 37\n" +
        "56 93 4",
        };

 







        public Day05Input(LogManager logManager)
        {
            _logger = logManager.GetLogger();
        }


        public void PopulateParsedSeeds()
        {
            List<BigInteger> seedInts = new List<BigInteger>();

            string[] seedArray = Seeds.Split(' ');

            foreach(string seedString in seedArray)
            {
                ParsedSeeds.Add(BigInteger.Parse(seedString));
            }
        }

        public List<BigInteger> GetParsedSeeds()
        {
            return ParsedSeeds;
        }

        public List<string> SplitMapValuesByLine(string mapString)
        {
            List<string> splitSeeds = new List<string>();

            string[] splitArray = mapString.Split('\n');

            foreach(string splitString in splitArray)
            {
                splitSeeds.Add(splitString);
            }
            return splitSeeds;
        }


        public MapValueStruct ParseMapStringToMapValueStruct(string line)
        {
            BigInteger destinationStart = 0;
            BigInteger sourceStart = 0;
            BigInteger range = 0;
            string[] seedArray = line.Split(' ');
            if (seedArray.Length < 3)
            {
                throw new ArgumentException($"Unable to parse 3 values in {nameof(ParseMapStringToMapValueStruct)}. _day05Input: {line}");
            }
            if (!BigInteger.TryParse(seedArray[0], out destinationStart))
            {
                throw new FormatException($"Failed to parse destinationStart: {seedArray[0]} in {nameof(ParseMapStringToMapValueStruct)}");
            }
            if (!BigInteger.TryParse(seedArray[1], out sourceStart))
            {
                throw new FormatException($"Failed to parse sourceStart: {seedArray[1]} in {nameof(ParseMapStringToMapValueStruct)}");
            }
            if (!BigInteger.TryParse(seedArray[2], out range))
            {
                throw new FormatException($"Failed to parse range: {seedArray[2]} in {nameof(ParseMapStringToMapValueStruct)}");
            }
            return new MapValueStruct(destinationStart, sourceStart, range);
        }


        public List<string> GetAllMapStrings()
        {
            List<string> allMapStrings = new List<string>();

            allMapStrings.AddRange(
                [
                    AllMaps[MapType.SeedToSoil],
                    AllMaps[MapType.SoilToFertilizer],
                    AllMaps[MapType.FertilizerToWater],
                    AllMaps[MapType.LightToTemperature],
                    AllMaps[MapType.TemperatureToHumidity],
                    AllMaps[MapType.HumidityToLocation]
                ]);

            return allMapStrings;
        }


        public SortedDictionary<MapType,string> GetAllMaps()
        {
            return AllMaps;
        }

        public SortedDictionary<MapType, string> GetQuestionTestMaps()
        {
            return QuestionTestMaps;
        }


        /*
        public BigInteger GetMaxValueFromMaps(SortedDictionary<MapType, string> maps)
        {
            BigInteger maxValue = 0;

            foreach (var mapEntry in maps)
            {
                List<string> mapLines = SplitMapValuesByLine(mapEntry.Value);
                foreach (string line in mapLines)
                {
                    BigInteger destinationStart, sourceStart, range;
                    ParseMapStringToBigInt(line, out destinationStart, out sourceStart, out range);

                    maxValue = BigInteger.Max(maxValue, destinationStart);
                    maxValue = BigInteger.Max(maxValue, sourceStart);
                    maxValue = BigInteger.Max(maxValue, range);
                }
            }

            return maxValue;
        }
        */







    }
}