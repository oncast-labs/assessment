package com.oncast.assessment.book.service;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import com.oncast.assessment.book.Book;
import com.oncast.assessment.book.Shelf;

public class OrderingService {
	
	private BookComparator[] orders = null;
	
	public static OrderingService getService(String filename) throws IOException {
		String sCurrentLine;

		BufferedReader reader = new BufferedReader(new FileReader(filename));

		try {
			List<String> ordersRequested = new ArrayList<String>();
			while ((sCurrentLine = reader.readLine()) != null) {
				String[] orderRequested = sCurrentLine.split(",");
				String order = orderRequested[0].toUpperCase() + "_" + orderRequested[1].toUpperCase();
				ordersRequested.add(order);
			}
			return new OrderingService(ordersRequested);
			
		} finally {
			reader.close();
		}
	}
	
	public List<Book> order(Shelf bookShelf) {
		return bookShelf.orderBy(orders);
	}
	
	private OrderingService(List<String> orders) {
		BookComparator[] ordersList = new BookComparator[orders.size()];
		
		for (int i = 0; i < orders.size(); i++) {
			ordersList[i] = BookComparator.valueOf(orders.get(i));
		}
		this.orders = ordersList;
	}
}