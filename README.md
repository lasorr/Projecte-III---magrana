# Dos Quarts (2/4) 🪓🌿
**Magrana Studios · Projecte III · Disseny Digital i Tecnologies Multimèdia 2025–2026**

> Joc competitiu de pantalla dividida per a dues jugadores. Transforma una ciutat capitalista i grisa en un espai comunitari i de colors — abans que ho faci la teva companya.

---

## Índex
- [Descripció](#descripció)
- [Transformacions](#transformacions)
- [Controls](#controls)
- [Scripts i mecàniques](#scripts-i-mecàniques)
- [Estructura del projecte](#estructura-del-projecte)
- [Equip](#equip)

---

## Descripció

**Dos Quarts** és un videojoc 3D emergent de món obert per a dues jugadores en pantalla dividida. Cada jugadora té una arma — un martell (verda) i una falç (rosa) — i ha de colpejar objectes especials a l'interior dels edificis per transformar-los. Qui acumuli més punts en acabar el temps guanya la partida.

El joc inclou:
- 4 tipus d'edificis transformables, cadascun amb mecànica pròpia
- Enemics (porclicies) que reverteixen les transformacions
- Edificis especials amb enemics a derrotar abans de poder transformar-los
- Power-ups positius i negatius
- 4 nivells: Tutorial → Nivell 1 → Nivell 2 → Mode Aleatori

**Plataforma:** PC (Windows / Mac / Linux)  
**Motor:** Unity 6 · URP  
**Estètica:** Pixel Art 3D

---

## Transformacions

| Edifici capitalista | Edifici transformat | Punts |
|---|---|---|
| Cafeteria d'especialitat | Botiga de fruita i verdura | +1 |
| Oficines corporatives | Parc | +1 |
| Col·legi catòlic | Casal de joves | +3 ⚠️ |
| Pisos d'especuladors (desnonament) | Comuna hippie | +3 ⚠️ |

> ⚠️ Els edificis especials requereixen derrotar 4 enemics abans de poder transformar-los.

---

## Controls

### Jugadora 1 — Noa (verda)
| Acció | Tecla |
|---|---|
| Moure's | `W` `A` `S` `D` |
| Colpejar | `G` |

### Jugadora 2 — Mar (rosa)
| Acció | Tecla |
|---|---|
| Moure's | `↑` `↓` `←` `→` |
| Colpejar | `Enter` |

### Global
| Acció | Tecla |
|---|---|
| Pausa | `Esc` |

> Compatible amb gamepad. Recomanat per a millor ergonomia amb dos jugadors al mateix teclat.

---

## Scripts i mecàniques

### `Colpejar.cs`
Afegit a l'arma de cada jugadora. Gestiona el `BoxCollider` de l'arma i detecta col·lisions amb:
- **Objectes especials** (`Especial`, `EspecialDesnon`, `EspecialMonja`) → envia cops al contador corresponent
- **Monges** → crida `Monjes.Morir()`
- **Porclicies de desnonament** → crida `PorcliciaDesnon.Derrotada()`
- **Porclicies IA** → incrementa `copsRebuts` al script `IAEnemicPorclicia`
- **Jugadora contrària** → activa el stun i l'animació `Emputjar`

Determina la propietat de l'arma (`propietariaArma = 1 o 2`) per tag (`Arma_1` / `Arma_2`) a `Start()`.

---

### `ContadorCops.cs`
Afegit als objectes especials dels edificis estàndard. Compta els cops rebuts per cada jugadora (per separat) i activa la transformació en arribar al llindar:
- **3 cops** en condicions normals
- **1 cop** amb superstar (power-up) actiu

Mostra indicadors visuals del progrés (`imatge1CopPrefab`, `imatge2CopPrefab`, `imatge3CopPrefab`). En transformar, instancia l'edifici comunista, li assigna propietaria via `PropietariaEdifici.SetPropietari()` i destrueix l'edifici capitalista.

Gestiona també el so ambiental de l'oficina en loop mentre hi ha una jugadora dins.

---

### `EdificiEspecialDesnon.cs`
Objecte especial del bloc de pisos amb desnonament. Bloquejat per cadenes fins que es derroten els 4 porclicies (`polisDerrotats >= 4`). Un cop desbloquejat, funciona igual que `ContadorCops` però suma **3 punts** al transformar. Inclou so ambiental de desnonament en loop i so de àvia contenta al transformar.

---

### `EdificiEspecialTrans.cs`
Objecte especial del col·legi catòlic. Bloquejat fins convertir les 4 monges (`monjesDerrotades >= 4`). Suma **3 punts** al transformar. Gestiona el so ambiental del cole en loop.

---

### `PorcliciaDesnon.cs`
Afegit a cada porclicia que bloqueja el desnonament. En ser colpejada, crida `EdificiEspecialDesnon.RegistrarPorcDerrotat()` i s'autodestrueix amb partícules.

---

### `Monjes.cs`
Afegit a cada monja del col·legi catòlic. En ser colpejada, instancia una drag queen (`dragg`) a la seva posició, la vincula com a filla de l'edifici capitalista, crida `EdificiEspecialTrans.RegistrarMonjaDerrotada()` i s'autodestrueix amb purpurina.

---

### `IAEnemicPorclicia.cs`
IA dels porclicies que sabotegen les transformacions. Utilitza `NavMeshAgent` per moure's pel mapa. Lògica:
- S'activa quan hi ha edificis comunistes (`EdificiComunista`) amb propietaria assignada
- **Escalat de dificultat** progressiu: velocitat i temps per destransformar augmenten amb el nombre d'edificis transformats (4 llindars: lent / mitjà / ràpid)
- En arribar a l'edifici, compta `tempsNecessari` segons i activa `PropietariaEdifici.edificiTransformat = true`
- Si rep 3 cops (`copsRebuts >= 3`), abandona l'objectiu actual i en busca un de nou
- No s'activa fins que la jugadora ha transformat almenys 5 edificis

---

### `PropietariaEdifici.cs`
Afegit a cada edifici comunista (transformat). Guarda qui és la propietaria (`1` o `2`) i quants punts val. En `Update()`, si `edificiTransformat = true` (activat per la IA), instancia l'edifici capitalista, resta punts via `TimeManager.RestaPunts()` i es destrueix.

---

### `PowerUp.cs`
Estrella positiva. En col·lisionar amb una jugadora, activa `superstarJug1/2` a **tots** els `ContadorCops` de l'escena durant 10 segons — reduint a 1 cop la transformació de qualsevol edifici.

---

### `PowerDown.cs`
Estrella negativa. En col·lisionar amb una jugadora, activa `stunJug = true` durant 3 segons, immobilitzant-la.

---

### `Moviment_jugadora.cs`
Moviment 360° amb `Rigidbody`. Llegeix el vector 2D del `InputSystem` i aplica velocitat directament a `linearVelocity`. La rotació s'aplica al `modelTransform` (fill) amb `Quaternion.Slerp` per suavitzar els girs. Gestiona:
- L'estat `stunJug`: atura el moviment i activa l'animació `Emputjar`
- L'estat `potMoure`: bloqueja el moviment durant l'animació d'atac (2.5s)
- So de passos en loop mentre la jugadora es mou

---

### `CamaraControl.cs`
Càmera en tercera persona que segueix la jugadora amb `Vector3.Lerp` per suavitzar el moviment. Offset configurable des de l'inspector. Es creen dues instàncies (una per jugadora) amb `Output Viewport` dividit en X per aconseguir la pantalla partida.

---

## Estructura del projecte

```
Assets/
├── Audio/              # AudioClips i GestioSo
├── Models 3D/          # FBX, textures i materials
│   ├── Models hito 3/
│   │   ├── Jugadora Rosa/
│   │   ├── Jugadora Verda/
│   │   ├── Porclicia/
│   │   └── ...
├── Prefabs/            # Edificis capitalistes i comunistes, power-ups, enemics
├── Scenes/             # Tutorial, Nivell1, Nivell2, Aleatori
├── Scripts/            # Tots els scripts .cs
├── UI/                 # Canvas, fonts, sprites d'interfície
└── Materials/          # Shader graph see-through i materials URP
```

---

## Equip

| Nom | Rol |
|---|---|
| Martina Bo | Disseny i programació |
| Júlia Martínez | Disseny i programació |
| Ovidiu Nerges | Modelat 3D i texturització |
| Laia Sorribes | Disseny i programació |

**Professor:** Josep Serrano i Mónica Martín  
**Assignatura:** Projecte III · DDTM · 2025–2026  
**GitHub:** [github.com/lasorr/Projecte-III---magrana](https://github.com/lasorr/Projecte-III---magrana)

---

*Magrana Studios © 2026 — Tots els models, textures i sons han estat creats per l'equip, excepte rigs i animacions de Mixamo.*
