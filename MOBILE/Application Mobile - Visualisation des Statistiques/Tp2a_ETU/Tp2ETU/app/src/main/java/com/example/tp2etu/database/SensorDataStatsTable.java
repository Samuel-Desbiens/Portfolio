package com.example.tp2etu.database;

/**
 * Classe gérant les modèle a utiliser pour les string SQLite.
 */
public class SensorDataStatsTable
{
    /**
     * Gère le SQLite pour créer une base de donnée.
     */
    public static final String CREATE_TABLE_SQL = "" +
            "CREATE TABLE IF NOT EXISTS TP2bPartie2 (" +
            "       id                  INTEGER          PRIMARY KEY       AUTOINCREMENT, " +
            "       sensorid            INTEGER, " +
            "       date                TEXT, " +
            "       low                 REAL, " +
            "       high                REAL  " +
            ")";

    /**
     * Gère le SQLite pour supprimer la base de donnée.
     */
    public static final String DROP_TABLE_SQL = "" +
            "DROP TABLE TP2bPartie2";

    /**
     * Gère le SQLite pour insérer dans la base de donnée.
     */
    public static final String INSERT_SQL = "" +
            "INSERT INTO TP2bPartie2 ( " +
            "        sensorid, " +
            "        date, " +
            "        low, " +
            "        high " +
            ") VALUES ( " +
            "        ?, " +
            "        ?, " +
            "        ?, " +
            "        ? " +
            ")";

    /**
     * Gère le SQLite pour avoir accès a tout les élement de la base de donnée.
     */
    public static final String SELECT_ALL_OF_SQL = "" +
            "SELECT " +
            "        TP2bPartie2.id, " +
            "        TP2bPartie2.sensorid, " +
            "        TP2bPartie2.date, " +
            "        TP2bPartie2.low, " +
            "        TP2bPartie2.high " +
            "FROM " +
            "        TP2bPartie2 ";

    /**
     * Gère le SQLite pour sélectionner une donnée de la base de donnée.
     */
    public static final String SELECT_ONE_OF_SQL = "" +
            "SELECT " +
            "        TP2bPartie2.id, " +
            "        TP2bPartie2.sensorid, " +
            "        TP2bPartie2.date, " +
            "        TP2bPartie2.low, " +
            "        TP2bPartie2.high " +
            "FROM " +
            "        TP2bPartie2 " +
            "WHERE " +
            "        TP2bPartie2.id = ?";

    /**
     * Gère le SQLite pour sélectionner la dernière donnée de la base de donnée.
     */
    public static final String SELECT_LAST_OF_SQL = "" +
            "SELECT " +
            "        MAX(id), " +
            "        TP2bPartie2.sensorid, " +
            "        TP2bPartie2.date, " +
            "        TP2bPartie2.low, " +
            "        TP2bPartie2.high " +
            "FROM " +
            "TP2bPartie2 ";

    /**
     * Gère le SQLite pour modifier une donnée dans la base de donnée.
     */
    public static final String UPDATE_SQL = "" +
            "UPDATE TP2bPartie2 " +
            "SET " +
            "        sensorid = ?, " +
            "        date = ?, " +
            "        low = ?, " +
            "        high = ? " +
            "WHERE " +
            "        id = ?";

    /**
     * Gère le SQLite pour supprimer une donnée de la base de donnée.
     */
    public static final String DELETE_SQL = "" +
            "DELETE FROM TP2bPartie2 " +
            "WHERE " +
            "        id = ?";

}
