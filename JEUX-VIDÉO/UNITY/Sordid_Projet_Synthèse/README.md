<div align="center">

![Projet Synthèse](.docs/Picture1.png)

# Sordid

</div>

Aidez Balthazar à sauver ses compagnons dans ce rogue like 2D platformer rempli de sorts et de gadgets uniques! Découvrez les secrets que renferment les donjons afin de devenir plus fort. Battez les associés du nécromancien dans des combat de boss palpitant et rempli d’action. 

<div align="center">

![Aperçu du Projet Synthèse](.docs/Picture1.png)

</div>

## Installation

[//]: # (TODO : Remplacez le nom de l'exécutable par celui dans ".gitlab-ci.yml". Voir la variable BUILD_NAME.)

Téléchargez la dernière version [stable du jeu][Stable Download]. Décompressez l'archive dans un dossier et exécutez
le programme `Balthazar.exe`. Notez que vous aurez à naviguer au travers de quelques sous-dossiers. Sous Windows,
vous aurez aussi un avertissement de sécurité que vous pouvez ignorer en cliquant sur le lien *Informations 
complémentaires* de la fenêtre.

## Démarrage rapide

Ces instructions vous permettront d'obtenir une copie opérationnelle du projet sur votre machine à des fins de 
développement.

### Prérequis

[//]: # (TODO : Modifier la version de Unity utilisée et ajouter tout autre logiciel nécessaire au développement.)
[//]: # (TODO : Notez que les hyperliens sont décrits à la fin de ce document.)

* [Git] - Système de contrôle de version. Utilisez la dernière version.
* [Rider] ou [Visual Studio] - IDE. Vous pouvez utiliser également n'importe quel autre IDE: assurez-vous simplement 
  qu'il supporte les projets Unity.
* [Unity 2023.1.9f1][Unity] - Moteur de jeu. Veuillez utiliser **spécifiquement cette version.** Attention à ne pas installer
  Visual Studio une seconde fois si vous avez déjà un IDE.

*Seul le développement sur Windows est complètement supporté.*

### Compiler une version de développement

Clonez le dépôt et ouvrez le projet dans Unity. Ensuite, ouvrez la scène `Menu` et appuyez sur le bouton *Play*.

### Compiler une version stable

Ouvrez le projet dans Unity. Ensuite, allez dans `File | Build Settings` et compilez le projet **dans un dossier vide**.

## Développement

Ces instructions visent à documenter comment contribuer au développement de certaines parties du projet. Les décisions
architecturales importantes seront aussi décrites ici.

### Fichiers auto-générés

Ce projet contient une bonne quantité de code généré automatiquement. Par exemple, la classe `Tags` est générée 
automatiquement à partir des *Tags* du projet Unity. Cependant, la génération en elle-même n'est pas automatique et
doit être lancée manuellement. Pour ce faire, accédez au menu `Tools | Harmony | Code Generator`. Une fenêtre devrait
s'ouvrir où vous pourrez générer le code lorsque nécessaire. 

Il est à noter que ce code est envoyé sur le dépôt. Soyez donc prudents lors des fusions entre les différentes branches
et préférez regénéner le code en cas de doutes.

![Générateur code Code](.docs/CodeGeneratorWindow.png)

Le générateur de code en lui-même est un exécutable externe et fait partie du *package* `Harmony`. Pour plus 
d'informations sur ce qu'il fait, consultez le [dépôt officiel][Harmony Code Generator].

### New Input System

Afin de simplifier la gestion des entrées, le [New Input System] est utilisé au lieu de celui intégré au moteur. Le 
fichier de configuration des entrées se trouve dans `Assets/Settings/InputActions`. Pour plus d'informations sur le 
nouveau *Input System*, consultez son [manuel d'utilisation][New Input System].

Il est à noter que ce fichier génère lui aussi du code C# (voir la classe `InputActions`) et que ce dernier est lui 
aussi inclus dans le dépôt. Soyez donc prudent lors des fusions.

![New Input System](.docs/NewInputSystem.png)

## Intégration Continue

Ce projet utilise [GitLab CI] pour effectuer de l'intégration continue avec les images *Docker* fournies par [GameCI].
Pour fonctionner, ces images ont besoin d'une license *Unity* valide. Cette license doit être ajouté dans la
variable d'environnement `UNITY_LICENSE` pour le projet.

Pour obtenir cette licence, le plus simple est d’utiliser la licence déjà présente sur votre ordinateur. Si ce n’est pas
déjà fait, effectuez l’activation d’une licence personnelle sur *UnityHub* et allez chercher le fichier à cet 
emplacement :

* Windows : `C:\ProgramData\Unity\Unity_lic.ulf`
* Mac : `/Library/Application Support/Unity/Unity_lic.ulf`
* Linux : `~/.local/share/unity3d/Unity/Unity_lic.ulf`

Sur GitLab, dans les paramètres `Settings | CI/CD | Variables`, ajoutez une nouvelle variable d’environnement nommée 
`UNITY_LICENSE` dont la valeur est le contenu du fichier de licence (le fichier `Unity_lic.ulf`). Ne cochez 
pas `Protect` ou `Mask` dans les options `Flags`.Une fois que c’est fait, dans UnityHub, refaites au complet le 
processus d’activation pour votre machine, car la licence que vous aviez avant sera désormais utilisée pour 
l’intégration continue.

## Éxécution des scripts d'intégration continue en mode local

Il est possible d'exécuter localement les scripts d'intégration continue pour le débogage. Dans une machine avec Docker,
commencez par cloner le projet. Ensuite, ajoutez un fichier nommé `UNITY_LICENSE` contenant le fichier de license (voir
comment l'obtenir à la section précédente). Enfin, démarrez un conteneur avec l'image d'intégration continue via la 
commande suivante.

```shell
# Naviguez dans le dossier du projet
cd /chemin/vers/votre/projet

# Démarrez le conteneur
docker run -it --rm \
-e UNITY_LICENSE="$(cat UNITY_LICENSE)" \
-v $(pwd):/project \
unityci/editor:2023.1.9f1-windows-mono-3.0.1 \
bash
```

Une fois à l'intérieur du conteneur, naviguez dans le projet et exécutez les différents scripts comme sur le véritable 
serveur d'intégration continue.

```shell
# Navigation vers le projet (monté dans le dossier /project).
cd /project
# Préparation de l'environnement.
./.gitlab-before.sh
# Éxécution du build
./.gitlab-build.sh
```

À la fin de l'exécution du *build*, vous trouverez un fichier journal nommé `build.log` contenant tous les détails sur
l'exécution du script. Cela devrait vous permettre de le déboguer. 

## Tester le projet

Vous êtes fortement encouragés à tester [la dernière version][Develop Download] de développement du jeu. Si vous 
rencontrez un bogue, vous êtes priés de le [signaler][Submit Bug] et de fournir une explication détaillée du problème 
avec les étapes pour le reproduire. Les captures d'écran et les vidéos sont les bienvenues.

## Contribuer au projet

Veuillez lire [CONTRIBUTING.md](CONTRIBUTING.md) pour plus de détails sur notre code de conduite.

## Auteurs

* **Sébastien Chouinard** - *Programmeur*
* **Xavier Leblond** - *Programmeur*
* **Antoine Tanguay** - *Programmeur*
* **Samuel Desbiens** - *Programmeur*
* **Benjamin Lemelin** - *Professeur en informatique*
  * Générateur de code. Voir [le dépôt][Harmony Code Generator] pour plus d'informations.

## Remerciements

* [GameCI] - Images Docker d'intégration continue pour *Unity*.
* [CreativeKind] - Sprites et animation du nécromancien.
* [Penzilla] - Sprites et animations de Balthazar.
* [Shade] - Sprites des objets et des sorts.
* [ClockWork Raven Studios] - Sprites des bottes.
* [Penusbmic] - Tileset, ennemis et Boss 1.
* [BDragon1727] - Sprites des sorts.



[//]: # (Hyperliens)
[Git]: https://git-scm.com/downloads
[Rider]: https://www.jetbrains.com/rider/
[Visual Studio]: https://www.visualstudio.com/fr/
[Unity]: https://unity3d.com/fr/get-unity/download/
[Harmony Code Generator]: https://gitlab.com/harmony-unity/code-generator
[New Input System]: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/index.html
[GitLab CI]: https://docs.gitlab.com/ee/ci/
[GameCI]: https://game.ci/
[Tiled]: https://www.mapeditor.org/
[CreativeKind]: https://creativekind.itch.io/
[Penzilla]: https://penzilla.itch.io/
[Shade] : https://merchant-shade.itch.io/
[ClockWork Raven Studios]: https://clockworkraven.itch.io/
[Penusbmic]: https://penusbmic.itch.io/
[BDragon1727]: https://bdragon1727.itch.io/

[//]: # (TODO : Modifiez ces hyperlien pour qu'ils ciblent votre projet à la place. Conservez le restant de l'URL.)
[//]: # (       Ici, vous devez remplacer "https://gitlab.com/csfpwmjv/projet-synthese/starter-game" par le votre.)
[Submit Bug]: https://gitlab.com/csfpwmjv/projet-synthese/starter-game/issues/new?issuable_template=Bug
[Stable Download]: https://gitlab.com/csfpwmjv/projet-synthese/starter-game/-/jobs/artifacts/master/download?job=build
[Develop Download]: https://gitlab.com/csfpwmjv/projet-synthese/starter-game/-/jobs/artifacts/develop/download?job=build