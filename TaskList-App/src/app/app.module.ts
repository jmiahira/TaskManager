/*Modules*/
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TooltipModule, ModalModule, BsDatepickerModule } from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';

/*Services*/
import { TasksService } from './_services/Tasks.service';

/*Components*/
import { AppComponent } from './app.component';
import { TasksComponent } from './tasks/tasks.component';
import { NavComponent } from './nav/nav.component';
import { DateTimeFormatPipePipe } from './_helper/DateTimeFormatPipe.pipe';

@NgModule({
   declarations: [
      AppComponent,
      TasksComponent,
      NavComponent,
      DateTimeFormatPipePipe
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule
   ],
   providers: [
      TasksService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
