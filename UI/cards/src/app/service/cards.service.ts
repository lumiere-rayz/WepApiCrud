import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Card } from '../models/card.model';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  baseUrl = 'https://localhost:7201/api/cards'
  constructor(private http: HttpClient) { }

  //Get all Cards
  getAllCards(): Observable<Card[]> {
    return this.http.get<Card[]>(this.baseUrl);
  }

  //add card
  addCard(card:Card): Observable<Card>{
    card.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Card>(this.baseUrl,card)
  }

  //deleten card
  deleteCard(id: string){
    return this.http.delete<Card>(this.baseUrl +'/'+ id);
  }

  //update a card
  updateCard(card: Card): Observable<Card>{
    return this.http.put<Card>(this.baseUrl +'/'+ card.id, card);
  }
}
