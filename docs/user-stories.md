# Slotly User Stories

Format: Rol olarak [ne] yapabilmek isterim, çünkü [fayda].
Öncelik: M = Must, S = Should, C = Could, W = Won't (bu sürümde)

## Customer (Müşteri)

| ID | Öncelik | Hikâye |
|----|---------|--------|
| C1 | M | Müşteri olarak işletmenin vitrin sayfasında hizmetleri, fiyatları ve çalışma saatlerini görmek isterim, çünkü randevu almadan önce karar vermek isterim. |
| C2 | M | Müşteri olarak e-posta ve parola ile kayıt olup giriş yapabilmek isterim, çünkü randevularım hesabımda saklansın. |
| C3 | M | Müşteri olarak bir hizmet seçip o hizmete uygun boş günleri ve saatleri görmek isterim, çünkü dolu saate randevu almaya çalışmak istemem. |
| C4 | M | Müşteri olarak seçtiğim saate randevu alabilmek isterim, çünkü telefonla aramadan işimi halletmek isterim. |
| C5 | M | Müşteri olarak aynı saati başkası aldıysa anlaşılır bir uyarı görmek isterim, çünkü randevumun kesinleştiğinden emin olmak isterim. |
| C6 | M | Müşteri olarak yaklaşan ve geçmiş randevularımı listeleyebilmek isterim, çünkü hangi günlere randevum olduğunu unutmak istemem. |
| C7 | M | Müşteri olarak randevumu iptal edebilmek isterim, çünkü planlarım değişebilir. |
| C8 | S | Müşteri olarak randevumu başka bir saate taşıyabilmek isterim, çünkü sıfırdan randevu almakla uğraşmak istemem. |
| C9 | S | Müşteri olarak randevu onayı ve hatırlatma e-postası almak isterim, çünkü randevumu kaçırmak istemem. |
| C10 | S | Müşteri olarak belirli bir uzmanı seçebilmek veya "fark etmez" diyebilmek isterim, çünkü bazen tercih ettiğim bir kişi vardır. |
| C11 | C | Müşteri olarak randevuyu takvimime (.ics) ekleyebilmek isterim, çünkü telefon takvimimde hatırlatma görmek isterim. |
| C12 | C | Müşteri olarak parolamı unuttuğumda sıfırlayabilmek isterim, çünkü hesabıma erişimimi kaybetmek istemem. |
| C13 | C | Müşteri olarak e-posta bildirimlerini açıp kapatabilmek isterim, çünkü istemediğim e-postaları almak istemem. |

## Owner (İşletme Sahibi)

| ID | Öncelik | Hikâye |
|----|---------|--------|
| O1 | M | İşletme sahibi olarak kategorimi seçerek işletmemi kaydedebilmek isterim, çünkü hazır hizmet şablonlarıyla hızlı başlamak isterim. |
| O2 | M | İşletme sahibi olarak müşteri girişinden ayrı bir panel girişi kullanmak isterim, çünkü yönetim ekranım müşterilerden gizli kalsın. |
| O3 | M | İşletme sahibi olarak hizmetlerimi (ad, süre, fiyat) ekleyip düzenleyebilmek isterim, çünkü sunduklarım zamanla değişir. |
| O4 | M | İşletme sahibi olarak çalışanlarımı ekleyip hangi hizmeti verebileceklerini belirleyebilmek isterim, çünkü herkes her işi yapmaz. |
| O5 | M | İşletme sahibi olarak çalışma saatlerini, mola aralıklarını ve izin günlerini tanımlayabilmek isterim, çünkü müsait saatler buna göre hesaplanmalı. |
| O6 | M | İşletme sahibi olarak gelen randevu taleplerini onaylayıp reddedebilmek (neden yazarak) isterim, çünkü takvimimin kontrolü bende olmalı. |
| O7 | M | İşletme sahibi olarak randevuları Yeni, Görülen, Yaklaşan, Geçmiş ve İptal sekmelerinde görmek isterim, çünkü hangi talebe henüz bakmadığımı net bilmek isterim. |
| O8 | S | İşletme sahibi olarak otomatik onay özelliğini açıp kapatabilmek isterim, çünkü bazı işletmeler her talebi elle onaylamak istemez. |
| O9 | S | İşletme sahibi olarak yeni talep gelince sayfayı yenilemeden anlık bildirim almak isterim, çünkü talebe hızlı cevap vermek isterim. |
| O10 | S | İşletme sahibi olarak bir randevuyu Tamamlandı veya Gelmedi olarak işaretleyebilmek isterim, çünkü gelmeyen müşterileri takip etmek isterim. |
| O11 | S | İşletme sahibi olarak bugünkü randevuları, bekleyen talepleri ve doluluk oranını dashboard'da görmek isterim, çünkü işimin durumunu tek bakışta anlamak isterim. |
| O12 | S | İşletme sahibi olarak randevularımı gün/hafta takvim görünümünde görmek isterim, çünkü günümü takvim olarak düşünürüm. |
| O13 | S | İşletme sahibi olarak iptal süresini ve rezervasyon penceresini ayarlayabilmek isterim, çünkü son dakika iptallerinden zarar görmek istemem. |
| O14 | C | İşletme sahibi olarak müşteri listemi ve her müşterinin gelmeme sayısını görmek isterim, çünkü sorunlu müşterilerden haberdar olmak isterim. |
| O15 | C | İşletme sahibi olarak logomu ve marka rengimi yükleyebilmek isterim, çünkü vitrin sayfam benim markama ait görünsün. |

## Staff (Çalışan)

| ID | Öncelik | Hikâye |
|----|---------|--------|
| S1 | S | Çalışan olarak giriş yapıp yalnızca kendi randevularımı görmek isterim, çünkü diğer çalışanların işi beni ilgilendirmez. |
| S2 | S | Çalışan olarak günlük ve haftalık takvimimi görmek isterim, çünkü gün planımı buna göre yaparım. |
| S3 | C | Çalışan olarak izin talebi oluşturabilmek isterim, çünkü tatil günlerimde randevu düşmesin. |

## PlatformAdmin (Platform Sahibi)

| ID | Öncelik | Hikâye |
|----|---------|--------|
| P1 | S | Platform sahibi olarak kayıtlı işletmelerin listesini görmek isterim, çünkü platformun büyümesini takip etmek isterim. |
| P2 | S | Platform sahibi olarak Hangfire panelini görmek isterim, çünkü arka plan işlerinin hatalarını yakalamak isterim. |
| P3 | C | Platform sahibi olarak sistem sağlık durumunu (health check) görmek isterim, çünkü sorunları kullanıcılardan önce fark etmek isterim. |

## Bu sürümde yapılmayacaklar (Won't)

| ID | Öncelik | Hikâye |
|----|---------|--------|
| W1 | W | Müşteri olarak randevu için depozito ödeyebilmek isterim, çünkü işletmeler gelmeme riskini azaltmak ister. |
| W2 | W | Müşteri olarak SMS ile hatırlatma almak isterim, çünkü e-postaya bakmayabilirim. |
| W3 | W | Müşteri olarak dolu saat için bekleme listesine yazılmak isterim, çünkü iptal olursa haber almak isterim. |
