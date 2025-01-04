package com.example.tp2etu.repository;

/**
 * Interface Repository pour gérer les connections entre l'interface et la/les bases de données.
 * @param <T> Un objet dans une base de donnée.
 */
public interface Repository<T>
{
    boolean insert(T[] dataObject);
    T find(int id);
    T findLast();
    T[] findAll();
    boolean save(T dataObject);
    boolean delete(T dataObject);
    boolean delete(int id);
}