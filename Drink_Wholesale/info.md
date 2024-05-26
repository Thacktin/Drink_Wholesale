Category Name
SubCaregory Category, Name
Product Producer, art-no, description, SubCategory, NetPrice, Inventory, Packaging

#Colleaque
#Order

Category - SubCategory 1 - N
SubCategory - Product  1 - N
Product - Order N-N


őkategóriák (név);
• Alkategóriák (főkategória, név)
• Termékek (gyártó, modellszám, leírás, kategória, nettó ár, raktárkészlet, kiszerelés (darabon kívül – ha
van));
• Munkatársak (teljes név, felhasználónév, jelszó);
• Rendelések (név, cím, telefonszám, e-mail cím, termékek listája, teljesítettség



subcategory:
	producer,artno,short.desc,netprice,brutprice
	#packaging sizes per product
	-Inventory
	-pagination ( 20)
	#sort by price and producer name(assc/desc)



Cart:
	-Product/product id
	-Quantity
	-Packaging




  • Az alkalmazott bejelentkezhet (felhasználónév és jelszó megadásával) a programba, illetve kijelentkezhet.
# • Bejelentkezve az alkalmazás listázza az egyes főkategóriákat és alkategóriákat, és egy alkategóriákon
	belül az ahhoz tartozó termékeket az alábbi adatok mutatásával:
#	gyártó, típusszám, rövid leírás, nettó és bruttó ár, elérhető kiszerelések és készlet.
#	– Egy adott terméknek módosítható a raktárkészlete.
#	– A felületen lehetőség van új termék megadására is, a gyártó, típusszám, rövid leírás, nettó ár,
	elérhető kiszerelések és készlet megadásával. A bruttó árat a rendszer automatikusan számolja
	(27% ÁFA).
#	• Az alkalmazás listázza a rendeléseket (dátum, név, cím, telefonszám, e-mail cím, termékek listája). A
	teljesítést a munkatárs kijelöléssel tudja kezdeményezni, amelyre a rendszer megerősítést kér.
	– A rendelések szűrhetőek a megrendelő név(részlet), dátum, valamint teljesítettség állapota szerint.
	– Egy adott rendelés kijelöléssel teljesítettnek jelölhető
	– Teljesítéskor a rendelt mennyiség automatikusan levonódik a készletből.
	– A teljesítés visszavonható, ebben az esetben a rendelt mennyiségek is visszahelyezésre kerülnek a
	készletbe.