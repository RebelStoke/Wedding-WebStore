import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  constructor() {
  }

  sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  async breath(){
    while (true){
      if (document.getElementById("arrow").style.width === "25%") {
        document.getElementById("arrow").style.width = "20%";
        await this.sleep(1000)
      } else{
        document.getElementById("arrow").style.width = "25%";
        await this.sleep(1000)
      }
    }
  }

  ngOnInit() {
    this.breath();
  }


}
