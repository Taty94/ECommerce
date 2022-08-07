import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'shop', loadChildren: () => import('./components/shop/shop.module').then(mod => mod.ShopModule)}, //para que mi shop module unicamente estara activo cuando ingrese al path shop
  {path: 'basket', loadChildren: () => import('./components/basket/basket.module').then(mod => mod.BasketModule)},
  {path: 'account', loadChildren: () => import('./components/account/account.module').then(mod => mod.AccountModule)},
  {path: 'checkout', loadChildren: () => import('./components/checkout/checkout.module').then(mod => mod.CheckoutModule)},
  {path: 'admin', loadChildren: () => import('./components/admin/admin.module').then(mod => mod.AdminModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
