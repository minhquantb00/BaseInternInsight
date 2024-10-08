import { computed } from 'vue';

export const paginationMeta = computed(() => {
  return (options, total) => {
    const start = (options.page - 1) * options.itemsPerPage + 1;
    const end = Math.min(options.page * options.itemsPerPage, total);
    return `Showing ${start} to ${end} of ${total} entries`;
  };
});
