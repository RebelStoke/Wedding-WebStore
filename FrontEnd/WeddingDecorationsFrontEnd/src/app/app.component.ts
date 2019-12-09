import {Component, NgModule} from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
@NgModule({
    imports: [NgbModule]
})
export class AppComponent {
  title = 'WeddingDecorationsFrontEnd';  model = {
    left: true,
    middle: false,
    right: false
  };
}
