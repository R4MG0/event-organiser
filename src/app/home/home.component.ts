import { Component, OnInit } from '@angular/core';
import { EventService } from '../shared/service/event.service';
import { Event } from '../shared/interfaces/event';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  events: Event[] = [];

  constructor(private readonly eventService: EventService, private readonly router: Router) { }

  ngOnInit(): void {
    // this.eventService.getEvents().subscribe((data) => {});
    this.events = this.eventService.getEvents();
  }
  goToDetails(id: string): void {
    this.router.navigate([`${id}`]);
  }

}
