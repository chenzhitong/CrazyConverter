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
                Green("请输入待转换的文本");
                var input = Console.ReadLine().Trim();

                //可能是公钥
                if (new Regex("^0[23][0-9a-f]{64}$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.PublicKeyToAddress(input);
                        Yellow("公钥转 Neo3 地址：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToScriptHash(Helper.PublicKeyToAddress(input)).big;
                        Yellow("公钥转脚本哈希（大端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToScriptHash(Helper.PublicKeyToAddress(input)).little;
                        Yellow("公钥转脚本哈希（小端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是 16 进制小端序字符串
                else if (new Regex("^([0-9a-f]{2})+$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.ScriptHashToAddress(input);
                        Yellow("脚本哈希转 Neo3 地址：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.HexNumberToBigInteger(input);
                        if (new Regex("^[0-9]{1,16}$").IsMatch(output))
                        {
                            Yellow("16 进制小端序字符串转大整数：");
                            Console.WriteLine(output);
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.HexStringToUTF8(input);
                        if (IsSupportedAsciiString(output))
                        {
                            Yellow("16 进制小端序字符串转 UTF8 字符串：");
                            Console.WriteLine(output);
                        }
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        var output = Helper.BigLittleEndConversion(input);
                        Yellow("小端序转大端序：");
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
                        Yellow("脚本哈希转 Neo3 地址：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.BigLittleEndConversion(input);
                        Yellow("大端序转小端序：");
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
                        Yellow("Neo 3 地址转脚本哈希（大端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToScriptHash(input).little;
                        Yellow("Neo 3 地址转脚本哈希（小端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToBase64String(input);
                        Yellow("Neo 3 地址转 Base64 脚本哈希：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是私钥
                else if (new Regex("^(L|K)[1-9a-km-zA-HJ-Z]{51}$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.PrivateKeyToPublicKey(input);
                        Yellow("私钥转公钥:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.PublicKeyToAddress(Helper.PrivateKeyToPublicKey(input));
                        Yellow("私钥转 Neo3 地址：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToScriptHash(Helper.PublicKeyToAddress(Helper.PrivateKeyToPublicKey(input))).big;
                        Yellow("私钥转脚本哈希（大端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.AddressToScriptHash(Helper.PublicKeyToAddress(Helper.PrivateKeyToPublicKey(input))).little;
                        Yellow("私钥转脚本哈希（小端序）:");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                }
                //可能是正整数
                else if (new Regex("^\\d+$").IsMatch(input))
                {
                    try
                    {
                        var output = Helper.BigIntegerToHexNumber(input);
                        Yellow("正整数转十六进制字符串：");
                        Console.WriteLine(output);
                    }
                    catch (Exception) { }
                    try
                    {
                        var output = Helper.BigIntegerToBase64String(input);
                        Yellow("正整数转 Base64 字符串：");
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
                            Yellow("Base64 脚本哈希转 Neo 3 地址：");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.AddressToScriptHash(Helper.Base64StringToAddress(input)).little;
                            Yellow("Base64 脚本哈希转脚本哈希（小端序）:");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.AddressToScriptHash(Helper.Base64StringToAddress(input)).big;
                            Yellow("Base64 脚本哈希转脚本哈希（大端序）:");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.Base64StringToBigInteger(input);
                            if (new Regex("^[0-9]{1,20}$").IsMatch(output))
                            {
                                Yellow("Base64 格式的字符串转大整数：");
                                Console.WriteLine(output);
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.Base64StringToString(input);
                            if (IsSupportedAsciiString(output))
                            {
                                Yellow("Base64 解码：");
                                Console.WriteLine(output);
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.ScriptsToOpCode(input);
                            if (output.Count > 0)
                            {
                                Yellow("脚本转 OpCode：");
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
                            var output = Helper.UTF8ToHexString(input);
                            Yellow("UTF8 字符串转十六进制字符串：");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                        try
                        {
                            var output = Helper.StringToBase64String(input);
                            Yellow("Base64 编码：");
                            Console.WriteLine(output);
                        }
                        catch (Exception) { }
                    }
                }
                Console.WriteLine();
            }
        }

        private static bool IsSupportedAsciiString(string input)
        {
            return input.All(p => p >= ' ' && p <= '~' || p == '\r' || p == '\n');
        }

        private static void Green(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void Yellow(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
