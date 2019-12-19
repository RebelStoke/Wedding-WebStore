import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor() {
  }

  ngOnInit() {
    window.onscroll = () => {
      if (window.scrollY > 35) {
          console.log(window.scrollY);
          document.getElementById('navbar').style.backgroundColor = '#faf7f2';
        } else {
        document.getElementById('navbar').style.backgroundColor = 'transparent';
      }
    };
  }
}
