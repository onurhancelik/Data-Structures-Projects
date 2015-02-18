using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proje1
{
    class Program
    {
        static Random r = new Random();

        static void Main(string[] args)
        {
            int secim; 
            Console.WriteLine("\n   1. İstatistiksel Sonuç Tablosu \n   2. Yıl veya yılların ay ve günlerindeki çakışmalar");
            Console.Write("\n Seçiminizi giriniz: ");
            do{
                secim = Convert.ToInt16(Console.ReadLine());
            } while (secim<1 || secim>2);

            int yıl1,yıl2;

            Console.WriteLine("\n Deney yapılacak yıl aralığını giriniz \n Tek bir yıl için aynı değeri yazınız. İlk olarak küçük yılı giriniz..\n");
            Console.Write("\t>> 1. yılı giriniz: ");
            yıl1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("\t>> 2. yılı giriniz: ");
            yıl2 = Convert.ToInt32(Console.ReadLine());

            int[] yıllar = new int[yıl2 - yıl1 + 1];

            int gecici = yıl1;
            for (int i = 0; i < yıl2 - yıl1 + 1; i++)
            {
                yıllar[i] = gecici;
                gecici++;
            }

            int[, ,] tarih = new int[31, 12, yıl2-yıl1+1]; //tarihleri tutan 3 boyutlu dizi oluşturuluyor.
            
            int[,] cakisma = new int[4,10]; //Farklı N değerleri için her 10 deneyin çakışmalarını tutan 2 boyutlu dizi oluşturuluyor.

            for(int a=0; a<4; a++) //4 farklı N için ayrı ayrı deney yapılıyor.
            {
                int n;
                Console.Write("\n >> "+(a+1)+". "+"N değerini giriniz: ");
                n = Convert.ToInt16(Console.ReadLine());

                for (int b = 0; b < 10; b++) //Her N değeri için 10 kere deney yapılıyor.
                {
                    n_rand_tarih(tarih,n,yıllar); //girilen yıl ve n değerlerine göre doğum tarihleri oluşturuluyor.

                    if (secim != 1) //Eğer her deneyin ayrıntısının görülmesi isteniyorsa bu if bloğunun içine giriyor.
                    {
                        Console.Write("\n>>> N=" + n + " için " + (b + 1) + ". deney <<<");
                        yazdır(tarih, yıllar);
                        Console.ReadLine();
                    }
                    for (int i = 0; i < 31; i++) 
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            for (int k = 0; k < yıllar.Length; k++)
                            {
                                if (tarih[i, j, k] > 1)
                                    cakisma[a,b] += tarih[i, j, k] - 1; //Her tarihe ait çakışma sayıları toplanıyor.
                            }
                        }
                    }
                    Array.Clear(tarih, 0, tarih.Length); //Her deney bittikçe tarih dizisi temizleniyor.
                }
            }

            istatistikYazdır(cakisma); //Programın en sonunda deneylerin çakışma sayıları ve ortalamaları yazdırılıyor.
            
            Console.ReadLine();
        }

        public static void rand_tarih(int[, ,] tarih,int[] yıllar) //Gelen yıllara göre 1 tane tarih üretiyor.
        {
            int ay, gün=0, yıl;

            ay = r.Next(12);
            yıl = r.Next(tarih.GetLength(2));

            if (yıllar[yıl] %4==0 && ay == 1)
                gün = r.Next(29);
            else
            {
                switch (ay)
                {
                    case 0: case 2: case 4: case 6: case 7: case 9: case 11: gün = r.Next(31); break;
                    case 1: gün = r.Next(28); break;
                    case 3: case 5: case 8: case 10: gün = r.Next(30); break;
                }
            }

            tarih[gün, ay, yıl]++;
        }

        public static void n_rand_tarih(int[, ,] tarih, int n,int[] yıllar) //n tane tarih üretiyor.
        {
            for (int i = 0; i < n; i++)
            {
                rand_tarih(tarih,yıllar);
            }
        }

        public static void yazdır(int[, ,] tarih, int[] yıllar) //12 satırlı her günde kaç doğum tarihinin olduğunu tablo halinde yazdıran metot. 
        {
            int i, j, k;
            string[] yılAdları = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            Console.WriteLine();
            Console.WriteLine();

            for (k = 0; k < tarih.GetLength(2); k++)
            {
                Console.WriteLine("   " + yıllar[k]);
                Console.Write("\t");
                for (int i2 = 0; i2 < tarih.GetLength(0); i2++)
                {
                    if(i2>8)
                    Console.Write((i2 + 1) + "");
                    else
                    Console.Write((i2 + 1) + " ");
                }
                Console.WriteLine();
                for (j = 0; j < tarih.GetLength(1); j++)
                {
                    Console.Write(yılAdları[j] + "\t");

                    for (i = 0; i < tarih.GetLength(0); i++) 
                    {
                        if (((i==30 && j==10)) || (yıllar[k] % 4 == 0 && j == 1 && i > 28) || (j == 1 && i > 27 && yıllar[k] % 4 != 0) || (i == 30 && (j == 3 || j == 5 || j == 8))) 
                            Console.Write("  "); //O ayda olmayan günleri boş geçiyor.
                        else
                            Console.Write(tarih[i, j, k] + " ");
                    }

                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static void istatistikYazdır(int[,] cakisma) //istatistikleri tablo halinde yazdıran metot.
        {
            Console.WriteLine("\n\n    İstatistiksel Değerler");
            Console.WriteLine("\n N>  50\t  100\t  500\t  1000");
            Console.WriteLine("     -------------------------");

            int [] ortTop = new int[4]; //4 farklı n değeri için ortalamaları tutacak olan dizi.

            for (int i = 0; i < 10; i++)
            {
                if (i < 9)
                {
                    Console.Write(" " + (i + 1) + ".  ");
                }
                else { Console.Write((i + 1) + ".  "); }
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(cakisma[j, i] + "\t  ");
                    ortTop[j]+= cakisma[j, i];
                }
                Console.WriteLine();
            }
            Console.Write("\nOrt. ");
            for (int i = 0; i < ortTop.Length; i++)
            {
                Console.Write((float)ortTop[i] / 10 + "   ");
            }
        }
    }
}
