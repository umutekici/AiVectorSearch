# AiVectorSearch

**AiVectorSearch**, yapay zeka tabanlı semantic vektör arama teknolojisini kullanan bir projedir. Bu proje, kullanıcının metin verisini anlamak için **semantic search** ve **vector embedding** tekniklerini uygular. Geliştiriciler ve veri bilimciler için metin arama ve veri analizi süreçlerini kolaylaştırır.

---

## Özellikler

- **Semantic Search**: Kullanıcı sorgularına en anlamlı sonuçları döndürmek için metin vektörlerini kullanır.
- **AI & Machine Learning**: Yapay zeka destekli doğal dil işleme (NLP) teknolojileri ile daha hassas aramalar yapar.
- **Vektör Tabanlı Arama**: Klasik kelime bazlı arama yerine, vektör benzerliğine dayalı arama yapılır.
- **Hızlı Sonuçlar**: Yüksek verimlilikle hızlı ve doğru sonuçlar alabilirsiniz.

---

## 🧪 Nasıl Çalıştırılır?

1. https://huggingface.co adresinden OpenAI API key'inizi alın.
2. `appsettings.json` dosyasına API key bilgilerini ekleyin. (Api Url olarak: https://api-inference.huggingface.co/pipeline/feature-extraction/sentence-transformers/all-MiniLM-L6-v2 bunu kullanabilirsiniz.)
3. `Products.txt` dosyasını `wwwroot/docs/` klasörüne koyun.
4. Projeyi çalıştırın.
5. `/api/document/ask?question="yaz için hafif spor ayakkabı" gibi bir istek atın.

---

## 📜 Lisans

Bu proje MIT lisansı altında yayınlanmıştır.  
İstediğiniz gibi kullanabilir, değiştirebilir ve dağıtabilirsiniz.
