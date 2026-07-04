document.addEventListener('DOMContentLoaded', () => {
  const $ = (selector, root = document) => root.querySelector(selector);
  const $$ = (selector, root = document) => Array.from(root.querySelectorAll(selector));
  const body = document.body;
  const toast = $('[data-toast-root]');
  let toastTimer;

  const normalize = (value) =>
    String(value || '').toLowerCase().normalize('NFD').replace(/[\u0300-\u036f]/g, '');

  const digits = (value, limit) => String(value || '').replace(/\D/g, '').slice(0, limit);

  function showToast(message) {
    if (!toast) return;
    toast.textContent = message || 'Acao concluida.';
    toast.classList.add('show');
    clearTimeout(toastTimer);
    toastTimer = setTimeout(() => toast.classList.remove('show'), 2600);
  }

  function mask(input) {
    if (input.dataset.mask === 'cpf') {
      input.value = digits(input.value, 11)
        .replace(/(\d{3})(\d)/, '$1.$2')
        .replace(/(\d{3})(\d)/, '$1.$2')
        .replace(/(\d{3})(\d{1,2})$/, '$1-$2');
      return;
    }

    const value = digits(input.value, 11);
    input.value = value.length <= 10
      ? value.replace(/(\d{2})(\d)/, '($1) $2').replace(/(\d{4})(\d)/, '$1-$2')
      : value.replace(/(\d{2})(\d)/, '($1) $2').replace(/(\d{5})(\d)/, '$1-$2');
  }

  function applyTurmaFilter(turmaSelect) {
    const turmaId = turmaSelect.value;
    [turmaSelect.dataset.filterStudents, turmaSelect.dataset.filterUcs].filter(Boolean).forEach((selector) => {
      const select = $(selector);
      if (!select) return;

      $$('option', select).forEach((option) => {
        if (!option.value) {
          option.hidden = false;
          option.disabled = false;
          return;
        }

        const visible = turmaId && option.dataset.turma === turmaId && option.dataset.searchHidden !== '1';
        option.hidden = !visible;
        option.disabled = !visible;
      });

      const selected = select.selectedOptions[0];
      if (!turmaId || (selected && selected.value && selected.dataset.turma !== turmaId)) {
        select.value = '';
      }
    });
  }

  function applyStudentSearch(input) {
    const select = $(input.dataset.studentSearch);
    if (!select) return;

    const query = normalize(input.value);
    $$('option', select).forEach((option) => {
      if (!option.value) return;
      const text = normalize(`${option.dataset.search || ''} ${option.textContent}`);
      option.dataset.searchHidden = query && !text.includes(query) ? '1' : '0';
    });

    const turmaSelect = $(input.dataset.turmaSource);
    if (turmaSelect) applyTurmaFilter(turmaSelect);
  }

  document.addEventListener('click', (event) => {
    const target = event.target;

    if (target.closest('.js-sidebar-toggle')) body.classList.toggle('sidebar-open');
    if (target.closest('.js-sidebar-close')) body.classList.remove('sidebar-open');

    const passwordButton = target.closest('[data-password-toggle]');
    if (passwordButton) {
      const input = $(passwordButton.dataset.passwordToggle);
      if (input) {
        input.type = input.type === 'password' ? 'text' : 'password';
        passwordButton.textContent = input.type === 'password' ? 'Mostrar' : 'Ocultar';
      }
    }

    const addUcButton = target.closest('[data-add-uc]');
    if (addUcButton) {
      const editor = addUcButton.closest('[data-uc-editor]');
      if (!editor) return;

      const list = $('[data-uc-list]', editor);
      const template = $(editor.dataset.ucTemplate);
      if (list && template) {
        list.appendChild(template.content.cloneNode(true));
        $('[data-uc-row]:last-child input', list)?.focus();
      }
    }

    const removeUcButton = target.closest('[data-remove-uc]');
    if (removeUcButton) {
      const list = removeUcButton.closest('[data-uc-list]');
      const row = removeUcButton.closest('[data-uc-row]');
      if (!list || !row) return;

      const rows = $$('[data-uc-row]', list);
      if (rows.length <= 1) $$('input', row).forEach((input) => { input.value = ''; });
      else row.remove();
    }

    const toastButton = target.closest('[data-toast]');
    if (toastButton) showToast(toastButton.dataset.toast);

    if (target.closest('[data-print]')) window.print();
  });

  document.addEventListener('input', (event) => {
    const input = event.target;

    if (input.matches('[data-mask]')) mask(input);
    if (input.matches('[data-student-search]')) applyStudentSearch(input);
  });

  $$('[data-turma-filter]').forEach((select) => {
    select.addEventListener('change', () => applyTurmaFilter(select));
    applyTurmaFilter(select);
  });

  $$('[data-student-search]').forEach(applyStudentSearch);

  $$('[data-table-filter]').forEach((input) => {
    const table = $(input.dataset.tableFilter);
    if (!table) return;

    const rows = $$('tbody tr', table);
    input.addEventListener('input', () => {
      const query = normalize(input.value);
      rows.forEach((row) => {
        row.style.display = normalize(row.textContent).includes(query) ? '' : 'none';
      });
    });
  });

  $$('[data-progress]').forEach((bar) => {
    const fill = $('span', bar);
    const value = Math.max(0, Math.min(100, Number(bar.dataset.progress) || 0));
    if (fill) requestAnimationFrame(() => { fill.style.width = `${value}%`; });
  });
});
