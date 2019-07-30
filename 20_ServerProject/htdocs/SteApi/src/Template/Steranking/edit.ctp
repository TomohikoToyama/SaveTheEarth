<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Steranking $steranking
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Form->postLink(
                __('Delete'),
                ['action' => 'delete', $steranking->Id],
                ['confirm' => __('Are you sure you want to delete # {0}?', $steranking->Id)]
            )
        ?></li>
        <li><?= $this->Html->link(__('List Steranking'), ['action' => 'index']) ?></li>
    </ul>
</nav>
<div class="steranking form large-9 medium-8 columns content">
    <?= $this->Form->create($steranking) ?>
    <fieldset>
        <legend><?= __('Edit Steranking') ?></legend>
        <?php
            echo $this->Form->control('Name');
            echo $this->Form->control('Score');
            echo $this->Form->control('Date');
        ?>
    </fieldset>
    <?= $this->Form->button(__('Submit')) ?>
    <?= $this->Form->end() ?>
</div>
