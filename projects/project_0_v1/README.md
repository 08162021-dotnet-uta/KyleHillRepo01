# Project 0 - Store Application

You are in charge of building a console-based Store Application.
You have free range on the products that can be sold in the store.

## Presentation

- you will be presenting on __Sept 1, 2021__
- you are expected to present at least the MVP requirements

## Technology Stack

- C#
- Serilog
- xUnit

## MVP

- a customer should be able to view set of store locations and select one
- a customer should be able to view available products to purchase
- a customer should be able to purchase at least 1 product
- a customer should be able to view past purchases
- a store should be able to view past purchases

## Requirements

The application is anchored around the Customer, Product, Order, Store objects.

### customer

- must be able to view past purchases
- must be able to view available store locations
- must be able to purchase 1 or more products
- must be able to update current purchase (add, edit, delete any purchase item)
- must be able to preview current purchase prior to checkout
- must be able to cancel a purchase

### product

### order

### store

- must be able to view past sales
- must be able to view sales by store location
- must be able to view sales by timespan (week, month, quarter)
- must be able to manage product inventory (add, edit, delete any product)

## Architecture

- Application (solution - sln template)
  + Client (executable - console template)
  + Domain (library - classlib template)
  + Storage (library - classlib template)
