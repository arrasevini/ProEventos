import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  public evento = {} as Evento;
  public form: FormGroup;
  public estadoSalvar = 'post';

  public get f(): any {
    return this.form.controls;
  }

  public get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toasrt: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public carregarEvento(): void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');
    if (eventoIdParam != null) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.eventoService.getEventoById(+eventoIdParam).subscribe(
        (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
        },
        (error: any) => {
          this.spinner.hide();
          this.toasrt.error('Erro ao tentar carregar Evento.', 'Erro!');
          console.error(error);
        },
        () => {
          this.spinner.hide();
        }
      );
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: [
        '',
        [Validators.required, Validators.min(1), Validators.max(120000)],
      ],
      //lote: ['', Validators.required],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {
      if (this.estadoSalvar == 'post') {
        this.evento = { ...this.form.value };
        this.eventoService.post(this.evento).subscribe(
          () => this.toasrt.success('Evento criado com sucesso.', 'Sucesso!'),
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toasrt.error('Erro ao salvar evento.', 'Erro!');
          },
          () => this.spinner.hide()
        );
      } else {
        this.evento = { id: this.evento.id, ...this.form.value };
        this.eventoService.put(this.evento).subscribe(
          () => this.toasrt.success('Evento salvo com sucesso.', 'Sucesso!'),
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toasrt.error('Erro ao salvar evento.', 'Erro!');
          },
          () => this.spinner.hide()
        );
      }
    }
  }
}
