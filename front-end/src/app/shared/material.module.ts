import { NgModule } from "@angular/core";

import {
  MatInputModule,
  MatCardModule,
  MatFormFieldModule,
  MatAutocompleteModule,
  MatChipsModule,
  MatButtonModule,
  MatIconModule,
  MatMenuModule,
  MatPaginatorModule,
  MatListModule,
  MatSnackBarModule,
  MatCheckboxModule,
  MatDialogModule
} from "@angular/material";

import { PlatformModule } from "@angular/cdk/platform";
import { ObserversModule } from "@angular/cdk/observers";

/**
 * NgModule that includes all Material modules that are required to serve the app.
 */
@NgModule({
  exports: [
    MatInputModule,
    ObserversModule,
    PlatformModule,
    MatCardModule,
    MatFormFieldModule,
    MatChipsModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatPaginatorModule,
    MatListModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatDialogModule,
    MatAutocompleteModule
  ]
})
export class CustomMaterialModule {}
