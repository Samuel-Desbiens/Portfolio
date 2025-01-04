import bcrypt 

#Petit script python pour pouvoir obtenir les hashs utilisé dams seed.sql

boromirPassword = "securepassword1234abcdefghijklmnopqrstuvwxyz"
boromirHash = bcrypt.hashpw(boromirPassword.encode('utf-8'), bcrypt.gensalt())
print(boromirHash)


flagPassword = "randompassword5678abcdefghijklmnopqrstuvw"
flagHash = bcrypt.hashpw(flagPassword.encode('utf-8'), bcrypt.gensalt())
print(flagHash)


pippinPassword = "theshire"
pippinHash = bcrypt.hashpw(pippinPassword.encode('utf-8'), bcrypt.gensalt())
print(pippinHash)


gandalfPassword = "strongpassword7890ABCDEFGHJKLMNOPQRSTUV"
gandalfHash = bcrypt.hashpw(gandalfPassword.encode('utf-8'), bcrypt.gensalt())
print(gandalfHash)