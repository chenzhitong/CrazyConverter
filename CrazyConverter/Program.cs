using Neo;
using Neo.Wallets;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrazyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("请输入待转换的文本");
                Console.ForegroundColor = ConsoleColor.White;
                var input = Console.ReadLine();

                //可能是公钥
                if (new Regex("^(0[23][0-9a-f]{64})+$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.PublicKeyToAddress(input);
                        Console.WriteLine("公钥转 Neo3 地址：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是 16 进制小端序字符串
                else if (new Regex("^([0-9a-f]{2})+$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.HexNumberToBigInteger(input);
                        if (new Regex("^[0-9]{1,16}$").IsMatch(output))
                        {
                            Console.WriteLine("16 进制小端序字符串转大整数：");
                            Console.WriteLine(output);
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.HexStringToAscii(input);
                        if (IsVisibleAsciiString(output))
                        {
                            Console.WriteLine("16 进制小端序字符串转 ASCII 字符串：");
                            Console.WriteLine(output);
                        }
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        var output = Helper.BigLittleEndScriptHashConversion(input);
                        Console.WriteLine("小端序转大端序：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是 16 进制大端序字符串
                else if (new Regex("^0x([0-9a-f]{2})+$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.ScriptHashToAddress(input);
                        Console.WriteLine("脚本哈希转 Neo3 地址：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.BigLittleEndScriptHashConversion(input);
                        Console.WriteLine("大端序转小端序：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是 Neo3 地址
                else if (new Regex("^N[K-Za-j][1-9a-km-zA-HJ-Z]{32}$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.AddressToScriptHash(input).big;
                        Console.WriteLine("Neo 3 地址转脚本哈希（大端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToScriptHash(input).little;
                        Console.WriteLine("Neo 3 地址转脚本哈希（小端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToBase64String(input);
                        Console.WriteLine("Neo 3 地址转 Base64 脚本哈希：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是正整数
                else if (new Regex("^\\d+$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.BigIntegerToBase64String(input);
                        Console.WriteLine("正整数转 Base64 字符串：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.BigIntegerToHexNumber(input);
                        Console.WriteLine("正整数转十六进制字符串：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                else
                {
                    //可能是 Base64 格式的字符串 或 普通字符串
                    if (new Regex("^([0-9a-zA-Z/+=]{4})+$").IsMatch(input))
                    {
                        try
                        {
                            var output = Helper.Base64StringToAddress(input);
                            Console.WriteLine("Base64 脚本哈希转 Neo 3 地址：");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.AddressToScriptHash(Helper.Base64StringToAddress(input)).little;
                            Console.WriteLine("Base64 脚本哈希转脚本哈希（小端序）:");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.AddressToScriptHash(Helper.Base64StringToAddress(input)).big;
                            Console.WriteLine("Base64 脚本哈希转脚本哈希（大端序）:");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.Base64StringToBigInteger(input);
                            if (new Regex("^[0-9]{1,20}$").IsMatch(output))
                            {
                                Console.WriteLine("Base64 格式的字符串转大整数：");
                                Console.WriteLine(output);
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.Base64StringToString(input);
                            if (IsVisibleAsciiString(output))
                            {
                                Console.WriteLine("Base64 解码：");
                                Console.WriteLine(output);
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.ScriptsToOpCode(input);
                            if (output.Count > 0)
                            {
                                Console.WriteLine("脚本转 OpCode：");
                                output.ForEach(p => Console.WriteLine(p));
                            }
                        }
                        catch (Exception) { }
                    }

                    //当做普通字符串处理
                    if (true)
                    {
                        try
                        {
                            var output = Helper.AsciiToHexString(input);
                            Console.WriteLine("ASCII 字符串转十六进制字符串：");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.StringToBase64String(input);
                            Console.WriteLine("Base64 编码：");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                    }
                }
                Console.WriteLine();
            }
        }

        private static bool IsVisibleAsciiString(string input)
        {
            return input.All(p => p >= ' ' && p <= '~');
        }
    }
}
