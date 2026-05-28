# 📖 Step-by-Step Guide til Business Tycoon 3D

## ✅ Trin 1: Åbn MainScene

1. Se på **Project** panelet (nederst venstre)
2. Klik på mappen **Assets**
3. Klik på mappen **Scenes**
4. **Dobbeltklik** på **MainScene.unity** for at åbne den

Du skulle nu se en scene med spilleren og lidt belysning.

---

## ✅ Trin 2: Opret World GameObject

1. I **Hierarchy** panelet (venstre side), **højreklik** i det tomme område
2. Vælg **Create Empty**
3. En ny GameObject oprettes - den hedder **GameObject**
4. **Omdøb** den til **World**:
   - Højreklik på den → Rename
   - Eller tryk **F2** når den er valgt
   - Skriv: `World`
   - Tryk **Enter**

---

## ✅ Trin 3: Tilføj WorldBuilder Script

1. Sørg for at **World** GameObject er valgt (klik på den)
2. Se på **Inspector** panelet (højre side)
3. Klik på **Add Component** knappen
4. Skriv `WorldBuilder` i søgeboksen
5. Vælg **WorldBuilder** scriptet
6. Klik på det

Nu er WorldBuilder scriptet tilføjet! ✨

---

## ✅ Trin 4: Kør Spillet

1. Tryk **Play** knappen (▶️) øverst i midten af Unity
2. Venter... verden bliver genereret automatisk! 🌍

Du skulle nu se:
- ✅ En grøn græsbund
- ✅ Veje (mørkegråt)
- ✅ Fortove (lysegråt)
- ✅ 5 farvede bygninger
- ✅ Træer 🌳
- ✅ Buske 🌿
- ✅ Gadelygter med lys 💡

---

## ✅ Trin 5: Kontroller Spilleren

I spilletilstand kan du:

- **W, A, S, D** - Gå rundt
- **Mus** - Se rundt
- **Højre museknap** - Hold og træk for at se dig omkring

Prøv at gå rundt og se dine bygninger! 🚶

---

## ✅ Trin 6: Stop Spillet

1. Tryk **Play** knappen (▶️) igen for at stoppe
2. Du er tilbage i editoren

---

## 🎉 Klart!

Din verden er nu oprettet! Du kan nu:
- Tilføje UI for at købe forretninger
- Tilføje AI-konkurrenter
- Forbedre grafikken med teksturer

Hvis du får fejl, tjek at:
- ✅ WorldBuilder scriptet er på World GameObject'en
- ✅ Scriptet ligger i `Assets/Scripts/World/`
- ✅ Du har gemt scenen før du trykker Play

Lykke til! 🚀
