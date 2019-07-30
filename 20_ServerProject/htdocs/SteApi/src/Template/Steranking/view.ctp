<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Steranking $steranking
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('Edit Steranking'), ['action' => 'edit', $steranking->Id]) ?> </li>
        <li><?= $this->Form->postLink(__('Delete Steranking'), ['action' => 'delete', $steranking->Id], ['confirm' => __('Are you sure you want to delete # {0}?', $steranking->Id)]) ?> </li>
        <li><?= $this->Html->link(__('List Steranking'), ['action' => 'index']) ?> </li>
        <li><?= $this->Html->link(__('New Steranking'), ['action' => 'add']) ?> </li>
    </ul>
</nav>
<div class="steranking view large-9 medium-8 columns content">
    <h3><?= h($steranking->Id) ?></h3>
    <table class="vertical-table">
        <tr>
            <th scope="row"><?= __('Name') ?></th>
            <td><?= h($steranking->Name) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Id') ?></th>
            <td><?= $this->Number->format($steranking->Id) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Score') ?></th>
            <td><?= $this->Number->format($steranking->Score) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Date') ?></th>
            <td><?= h($steranking->Date) ?></td>
        </tr>
    </table>
</div>
