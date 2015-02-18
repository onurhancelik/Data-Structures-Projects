using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proje_1._2
{

    class Öğrenci //Öğrencilere ait bilgileri içinde barındıran Öğrenci adında class.
    {
        static Random r = new Random();

        private string ad;
        private string soyad;
        private int dNotu;
        public Öğrenci() { }
        public Öğrenci(string ad, string soyad) { this.ad = ad; this.soyad = soyad; dNotu = r.Next(40, 101); }
        //ad ve soyad'ı parametre alıp dNotu otomatik random atayan constructor.
        public string adAl() { return ad; }
        public void adAta(string adı) { ad = adı; }
        public string soyadAl() { return soyad; }
        public void soyadAta(string soyadı) { soyad = soyadı; }
        public int notAl() { return dNotu; }

        public void yazdir()
        { Console.WriteLine("\t" + ad + " " + soyad + "\t" + dNotu); }
    }

    class Ülke //Ülkelere ait bilgileri içinde barındıran Ülke adında class.
    {
        private string ad;
        private int kontenjan;
        public Öğrenci[] ogrDizi;
        public Ülke() { }
        public Ülke(string ad) { this.ad = ad; } //ad'ı paramatre olarak alan constructor.

        public string adAl() { return ad; }
        public int kontenjanAl() { return kontenjan; }
        public void kontenjanAta(int kont) { kontenjan = kont; }
    }

    class Program
    {
        static Random r = new Random();

        static void Main(string[] args)
        {
            int toplamKontenjan;
            Ülke[] ülkeler = new Ülke[9]; //Ülke tipinde ülkeleri tutan dizi.

            toplamKontenjan = ulkeOlustur(ülkeler); //ulkeOlustur metodu ile daha önce oluşturulan ülkeler dizisi atanıyor. 
            //Aynı zamanda ülkelerin toplam kontenjanı metottan geliyor.
            Öğrenci[] öğrenciler;
            öğrenciler = ogrOlustur(); //ogOlustur metotundan Öğrenci tipinde dizi dönüyor ve öğrenciler dizisine eşitleniyor.

            ogrYerlestir(toplamKontenjan, öğrenciler, ülkeler); //Öğrenciler ülkelerin kontenjanlarına göre yerleştiriliyor.

            ülkeYazdır(ülkeler, toplamKontenjan, öğrenciler); //Yazdırma işlemleri yapılıyor.

            Console.ReadLine();
        }

        public static int ulkeOlustur(Ülke[] ülkeler)
        {
            string[] ülkeİsimleri = { "ENG", "GER", "FRE", "ITA", "ESP", "USA", "JAP", "CHN", "RUS" };

            for (int i = 0; i < 9; i++)
                ülkeler[i] = new Ülke(ülkeİsimleri[i]);

            int secim, topKota = 0;
            Console.Write("Ülke kontenjanlarını belirlemek için secenek giriniz (1-Custom / 2-Random): ");
            secim = sayiAl(1, 2);

            switch (secim)
            {
                case 1: //Eğer ülke kontenjanlarını kullanıcı girecekse case 1 çalışıyor.
                    for (int i = 0; i < 9; i++)
                    {
                        Console.Write(ülkeler[i].adAl() + " için kontenjan sayısını (0-10) giriniz: ");
                        ülkeler[i].kontenjanAta(sayiAl(0, 10));
                        topKota += ülkeler[i].kontenjanAl();
                        ülkeler[i].ogrDizi = new Öğrenci[ülkeler[i].kontenjanAl()];

                        for (int j = 0; j < ülkeler[i].kontenjanAl(); j++)
                            ülkeler[i].ogrDizi[j] = new Öğrenci(); //Ülke sınıfı tipindeki değişkenin içinde bulunun Öğrenci tipindeki dizi için bellekten yer ayrılıyor.
                    }
                    break;

                case 2: //Eğer ülke kontenjanları random olarak belirlenecekse case 2 çalışıyor.
                    for (int i = 0; i < 9; i++)
                    {
                        ülkeler[i].kontenjanAta(r.Next(0, 11));
                        topKota += ülkeler[i].kontenjanAl();
                        ülkeler[i].ogrDizi = new Öğrenci[ülkeler[i].kontenjanAl()];

                        for (int j = 0; j < ülkeler[i].kontenjanAl(); j++)
                            ülkeler[i].ogrDizi[j] = new Öğrenci(); //Bellekten yer ayrılıyor.
                    }
                    break;
            }
            return topKota;
        }

        public static Öğrenci[] ogrOlustur()
        {
            string[] adlar = { "Fatma", "Ahmet", "Ayşe", "Hüseyin", "Emine", "Hasan", "Hatice", "İbrahim", "İsmail", "Osman", "Yusuf", "Murat", "Ömer", "Ramazan", "Halil", "Salih", "Sultan", "Zehra", "Hanife", "Merve" };
            string[] soyadlar = { "Yıldız", "Sarı", "Öztürk", "Aydın", "Özdemir", "Aslan", "Doğan", "Kılıç",
                                    "Polat", "Çetin", "Kara", "Koçak", "Kurt", "Özkan", "Şimşek","Çelik","Üstün","Yılmaz","Şahin","Kıral"};

            string[] isimler = new string[400];

            int k = 0; //k değişkeni isimler dizisinin indisini tutuyor.
            for (int i = 0; i < adlar.Length; i++) //20 ad ve soyad çapraz şekilde karıştırılarak 400 farklı isim oluşturuluyor.
            {
                for (int j = 0; j < soyadlar.Length; j++)
                {
                    isimler[k] = adlar[i] + " " + soyadlar[j]; //ad ve soyadlar isimler dizisine atanıyor.
                    k++;
                }
            }

            int basvuranOgr;
            Console.Write("\nBaşvuran öğrenci sayısını (1-150) giriniz: ");

            basvuranOgr = sayiAl(1, 150);

            Öğrenci[] öğrenciler = new Öğrenci[basvuranOgr];

            int altmisUstu = 0; //60 ve üstünde notu olan öğrenci sayısını tutuyor.

            for (int i = 0; i < öğrenciler.Length; i++)
            {
                int t;
                do
                {
                    t = r.Next(0, 400);
                    if (isimler[t] != null)
                    {
                        string[] gecici = isimler[t].Split(); //isimler ad ve soyad olarak ayrılıyor.
                        öğrenciler[i] = new Öğrenci(gecici[0], gecici[1]);
                    }
                } while (isimler[t] == null); //isimler dizisinden null olmayanı bulana kadar arıyor.

                Array.Clear(isimler, t, 1); //Kullanılan ismi diziden siliyor.
                if (öğrenciler[i].notAl() >= 60)
                    altmisUstu++;
            }

            Öğrenci[] basarılılar = new Öğrenci[altmisUstu]; //60 ve üstü notu olan öğrencilerin dizisi oluşturuluyor.

            int b = 0; //basarılılar dizisinin indisini tutuyor.
            for (int i = 0; i < öğrenciler.Length; i++)
            {
                if (öğrenciler[i].notAl() >= 60) //60 ve üstü notu olan öğrenciler, basarılılar dizisine alınıyor.
                {
                    basarılılar[b] = new Öğrenci();
                    basarılılar[b] = öğrenciler[i];
                    b++;
                }
            }
            return basarılılar; //basarılılar dizisi ana programa gönderiliyor.
        }

        public static int sayiAl(int alt, int ust) //Parametre olarak gelen alt ve üst sınır değerlerine göre integer tipinde değer döndüren metot.
        {
            int sayi = Convert.ToInt16(Console.ReadLine());

            while (sayi < alt || sayi > ust)
            {
                Console.Write("Hatalı veri girdisi! Girdiyi " + alt + "-" + ust + " aralığında giriniz: ");
                sayi = Convert.ToInt16(Console.ReadLine());
            }
            return sayi;
        }

        public static void sirala(Öğrenci[] dizi) //Öğrenci tipindeki diziyi öğrencilerin notlarına göre büyükten küçüğe sıralayan metot.
        {
            for (int i = 0; i < dizi.Length - 1; i++) //Bubble Sort
            {
                for (int j = 1; j < dizi.Length - i; j++)
                {
                    if (dizi[j].notAl() > dizi[j - 1].notAl())
                    {
                        Öğrenci gecici = dizi[j - 1];
                        dizi[j - 1] = dizi[j];
                        dizi[j] = gecici;
                    }
                }
            }
        }

        public static void ulkeSirala(Ülke[] dizi) //Ülke tipindeki diziyi ülkelerin kontenjan sayılarına göre büyükten küçüğe sıralayan metot.
        {
            for (int i = 0; i < dizi.Length - 1; i++) //Buble Sort
            {
                for (int j = 1; j < dizi.Length - i; j++)
                {
                    if (dizi[j].kontenjanAl() > dizi[j - 1].kontenjanAl())
                    {
                        Ülke gecici = dizi[j - 1];
                        dizi[j - 1] = dizi[j];
                        dizi[j] = gecici;
                    }
                }
            }
        }

        public static void ogrYerlestir(int topKota, Öğrenci[] öğrenciler, Ülke[] ülkeler)
        {
            int l = 0, k = 0; // l ve k değişkenleri Ülke tipindeki değişkende bulunun ogrDizi ve öğrenciler dizilerinin indislerini tutuyor.
            if (öğrenciler.Length > topKota) //Eğer yerleşmeye hak kazanan öğrenci sayısı toplam kontenjandan fazlaysa bu blok çalışıyor.
            {
                sirala(öğrenciler); //Öğrenciler notlarına göre sıralanır.
                for (int i = 0; i < ülkeler.Length; i++)
                {
                    l = 0; //Her ülke için l değeri 0'lanır.
                    if (ülkeler[i].kontenjanAl() != 0)
                    {
                        while (l < ülkeler[i].kontenjanAl()) //While döngüsü l indisinin ülke kontenjanı sınırına kadar döner.
                        {
                            ülkeler[i].ogrDizi[l] = öğrenciler[k];
                            l++;
                            k++; //sonraki öğrenci.
                        }
                    }
                }
            }
            else //Eğer kontenjan sayısı başarılı öğrencilerin sayısından fazlaysa.
            {
                float oran = (float)öğrenciler.Length / topKota; //Eşit yüzdeyle öğrencileri yerleştirmek için belirlenen oran.
                ulkeSirala(ülkeler); //ülkeler kontenjanlarına göre sıralanır.
                float gidenOgrSay;
                for (int i = 0; i < ülkeler.Length; i++)
                {
                    l = 0;
                    if (ülkeler[i].kontenjanAl() != 0)
                    {
                        gidenOgrSay = ülkeler[i].kontenjanAl() * oran; //Eşit yüzdeyi sağlamak için yapılan işlem.
                        while (l < gidenOgrSay)                      //Ülkeye gidecek olan öğrenci sayısını float tipinde belirler.
                        {
                            if (k < öğrenciler.Length)
                            {
                                ülkeler[i].ogrDizi[l] = öğrenciler[k];
                                k++; //sonraki öğrenci.
                            }
                            l++;
                        }
                    }
                }
            }
        }

        public static void ülkeYazdır(Ülke[] ülkeler, int topKota, Öğrenci[] öğrenciler)
        {
            Console.WriteLine();
            Console.WriteLine("Kazanan Öğrenciler:");
            for (int i = 0; i < ülkeler.Length; i++) //Her ülke için(kontenjanı 0 olmayan), o ülkeye giden öğrencilerin listesi yazdırılır.
            {
                if (ülkeler[i].kontenjanAl() != 0 && ülkeler[i].ogrDizi[0].adAl() != null)
                {
                    int yerlesen = 0;
                    Console.WriteLine();
                    for (int j = 0; j < ülkeler[i].kontenjanAl(); j++)
                    {
                        if (ülkeler[i].ogrDizi[j].adAl() != null)
                            yerlesen++;
                    }
                    Console.WriteLine(ülkeler[i].adAl() + "> Kontenjan Sayısı: " + ülkeler[i].kontenjanAl()+"\n     Yerleşen Öğrenci Sayısı: "+yerlesen+"\n");
                    for (int j = 0; j < ülkeler[i].kontenjanAl(); j++)
                    {
                        if (ülkeler[i].ogrDizi[j].adAl() != null)
                            ülkeler[i].ogrDizi[j].yazdir();
                    }
                }
            }

            if (öğrenciler.Length > topKota) //Eğer kazanamayan öğrenci varsa bu blok çalışır.
            {
                Console.WriteLine();
                Console.WriteLine("Kazanamayan Öğrencilerin Sayısı: " + (öğrenciler.Length - topKota));
                Console.WriteLine();
                Console.WriteLine("Kazanamayan Öğrenciler:\n");
                for (int i = topKota; i < öğrenciler.Length; i++)
                    öğrenciler[i].yazdir();
            }
        }
    }
}