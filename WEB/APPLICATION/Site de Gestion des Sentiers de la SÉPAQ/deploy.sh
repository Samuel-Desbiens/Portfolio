#!/usr/bin/env sh

# Les étapes de ce script peuvent être exécutées manuellement.

# Pour le script, arrêt si erreur
set -e

# ÉTAPE 1 - compiler le code
pnpm run build

# ÉTAPE 2 - Aller dans le dossier "dist"
cd dist

# ÉTAPE 3 - Créer un repo dans le dossier "dist", ajouter le contenu et faire un "commit"
git init
git add -A
git commit -m 'deploy'

# Étape 4 - Déployer le site comme page GitHub
#                              ____ nom du repo
#                             V
git push -f git@github.com:TheMostSwagDuck/Tp3AppWeb.git master:Page
#                                                                  ^
#                             nom de la branche d'une page GitHub__|
cd -
