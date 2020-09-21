import { Component, OnInit } from '@angular/core';
import { EIAPointsService } from "../services/EIAPointsService";
import { EIAPoint } from "../models/EIAPoint";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  points: EIAPoint[];

  constructor(private eiaPointsService: EIAPointsService) {

  }

  ngOnInit(): void {
    this.eiaPointsService.getAllPoints().subscribe((eiaPoints) => {
      this.points = eiaPoints;
    });
  }
}
