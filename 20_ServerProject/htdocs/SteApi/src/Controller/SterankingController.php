<?php
namespace App\Controller;

use App\Controller\AppController;

/**
 * Steranking Controller
 *
 * @property \App\Model\Table\SterankingTable $Steranking
 *
 * @method \App\Model\Entity\Steranking[]|\Cake\Datasource\ResultSetInterface paginate($object = null, array $settings = [])
 */
class SterankingController extends AppController
{
    /**
     * Index method
     *
     * @return \Cake\Http\Response|null
     */
    public function index()
    {
        $steranking = $this->paginate($this->Steranking);

        $this->set(compact('steranking'));
    }

    /**
     * View method
     *
     * @param string|null $id Steranking id.
     * @return \Cake\Http\Response|null
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function view($id = null)
    {
        $steranking = $this->Steranking->get($id, [
            'contain' => []
        ]);

        $this->set('steranking', $steranking);
    }

    /**
     * Add method
     *
     * @return \Cake\Http\Response|null Redirects on successful add, renders view otherwise.
     */
    public function add()
    {
        $steranking = $this->Steranking->newEntity();
        if ($this->request->is('post')) {
            $steranking = $this->Steranking->patchEntity($steranking, $this->request->getData());
            if ($this->Steranking->save($steranking)) {
                $this->Flash->success(__('The steranking has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The steranking could not be saved. Please, try again.'));
        }
        $this->set(compact('steranking'));
    }

    /**
     * Edit method
     *
     * @param string|null $id Steranking id.
     * @return \Cake\Http\Response|null Redirects on successful edit, renders view otherwise.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function edit($id = null)
    {
        $steranking = $this->Steranking->get($id, [
            'contain' => []
        ]);
        if ($this->request->is(['patch', 'post', 'put'])) {
            $steranking = $this->Steranking->patchEntity($steranking, $this->request->getData());
            if ($this->Steranking->save($steranking)) {
                $this->Flash->success(__('The steranking has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The steranking could not be saved. Please, try again.'));
        }
        $this->set(compact('steranking'));
    }

    /**
     * Delete method
     *
     * @param string|null $id Steranking id.
     * @return \Cake\Http\Response|null Redirects to index.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function delete($id = null)
    {
        $this->request->allowMethod(['post', 'delete']);
        $steranking = $this->Steranking->get($id);
        if ($this->Steranking->delete($steranking)) {
            $this->Flash->success(__('The steranking has been deleted.'));
        } else {
            $this->Flash->error(__('The steranking could not be deleted. Please, try again.'));
        }

        return $this->redirect(['action' => 'index']);
    }

     public function getRanking()
	{
		$this->autoRender	= false;
		
		//�e�[�u�����烉���L���O���X�g���Ƃ��Ă���
        $query	= $this->Steranking->find("all");

        //�N�G���[�������s���B
        $query->order(['Score'=>'DESC']);   //�~��
        $query->limit(10);                  //�擾������10���܂łɍi��
		
		//json�ɃV���A���C�Y����B
		$json	= json_encode($query);

		//json�f�[�^��Ԃ��B�i���X�|���X�Ƃ��ĕ\������B�j
		echo $json;
    }

    public function setRanking()
    {
		$this->autoRender	= false;

        //POST �p�����[�^���擾
        $postName   = $this->request->data("Name");
        $postScore  = $this->request->data("Score");

        $record = array(
            "Name"=>$postName,
            "Score"=>$postScore,
            "Date"=>date("Y/m/d H:i:s")
        );

        //�e�[�u���Ƀ��R�[�h��ǉ�
        $prm1    = $this->Steranking->newEntity();
        $prm2    = $this->Steranking->patchEntity($prm1,$record);
        
        if( $this->Steranking->save($prm2) ){
            echo "Success";   //����
        }else{
            echo "Error";   //���s
        }
    }
}
